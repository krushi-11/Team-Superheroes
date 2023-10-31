using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json;

using ContosoCrafts.WebSite.Models;

using Microsoft.AspNetCore.Hosting;

namespace ContosoCrafts.WebSite.Services
{
   public class JsonFileProductService
    {
        public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json"); }
        }

        public IEnumerable<ProductModel> GetProducts()
        {
            using(var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<ProductModel[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

        /// <summary>
        /// Add Rating
        /// 
        /// Take in the product ID and the rating
        /// If the rating does not exist, add it
        /// Save the update
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="rating"></param>
        public bool AddRating(string productId, int rating)
        {
            var products = GetProducts();
            // If the ProductID is invalid, return
            if (string.IsNullOrEmpty(productId))
            {
                return false;
            }

            

            // Look up the product, if it does not exist, return
            var data = products.FirstOrDefault(x => x.Id.Equals(productId));
            if (data == null)
            {
                return false;
            }

            // Check Rating for boundaries, do not allow ratings below 0
            if (rating < 0)
            {
                return false;
            }

            // Check Rating for boundaries, do not allow ratings above 5
            if (rating > 5)
            {
                return false;
            }

            // Check to see if the rating exist, if there are none, then create the array
            if (data.Ratings == null)
            {
                data.Ratings = new int[] { rating };
            }

            // Add the Rating to the Array
            var ratings = data.Ratings.ToList();
            ratings.Add(rating);
            data.Ratings = ratings.ToArray();

            // Save the data back to the data store
            SaveProducts(products);

            return true;
        }

        // Update Data Method
        public ProductModel UpdateData(ProductModel updatedProduct)
        {
            if(updatedProduct!= null)
            {
                var products = GetProducts();
                var productData = products.FirstOrDefault(x=>x.Id.Equals(updatedProduct.Id));

                productData.Title = updatedProduct.Title;
                productData.Description = updatedProduct.Description;
                productData.Url = updatedProduct.Url;
                productData.Image = updatedProduct.Image;

                SaveProducts(products);
                return productData;
            }
            else 
            {
                System.Console.WriteLine("Data is null");
                return null; 
            }
        }

        // Create Data Method
        public ProductModel CreateData()
        {
            var data = new ProductModel()
            {
                Id = System.Guid.NewGuid().ToString(),
                Title = "Enter Title",
                Description = "Enter Description",
                Url = "Enter Url",
                Image = "",
            };

            var newData = GetProducts().Append(data);

            SaveProducts(newData);
            return data;

        }

       // Save product after convertinbg it to json format
        private void SaveProducts(IEnumerable<ProductModel> products)
        {
            var jsonProducts = JsonSerializer.Serialize(products,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(JsonFileName, jsonProducts);
        }

        // Delete product 
        public bool DeleteData(string id)
        {
            // Get the current set, and remove the record with the specified ID from it
            var products = GetProducts().ToList();
            var productToDelete = products.FirstOrDefault(x => x.Id.Equals(id));
            if (productToDelete != null){
                products.Remove(productToDelete);
                SaveProducts(products);
                return true;
            
            }else{
                return false; 
            }
            

        }
    }
}