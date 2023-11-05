using System;
using System.Collections.Generic;
using System.Drawing;
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
            get
            { 
                return Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json"); // Path to Json File
            }
        }

        public IEnumerable<ProductModel> GetProducts() // Get all Products from Json File
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
        public bool AddRating(string productId, int rating) // Add Rating Method
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
                var products = GetProducts(); // Get the Products
                var productData = products.FirstOrDefault(x=>x.Id.Equals(updatedProduct.Id)); // Filter by id

                productData.Title = updatedProduct.Title; // Set Old Title to Updated Title
                productData.Description = updatedProduct.Description; // Set Old Description to Updated Description
                productData.Url = updatedProduct.Url; // Set Old Url to Updated Url
                productData.Image = updatedProduct.Image; // Set Old Image to Updated Image
                productData.Price = updatedProduct.Price; // Set Old Price to Updated Price
                productData.Stock = updatedProduct.Stock; // Set Old Stock to Updated Stock

                SaveProducts(products); // Save the Updated List

                return productData;
        }


        // Create Data Method
        public ProductModel CreateData(ProductModel productModel)
        {
            productModel.Id = System.Guid.NewGuid().ToString();

            // Get the current set, and append the new record to it becuase IEnumerable does not have Add
            var dataSet = GetProducts();
            dataSet = dataSet.Append(productModel);

            // Update the products.json with newly created products
            SaveProducts(dataSet);

            //Return newly created data
            return productModel;
        }


        // Save product after converting it to json format
        private void SaveProducts(IEnumerable<ProductModel> products)
        {
            var jsonProducts = JsonSerializer.Serialize(products,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(JsonFileName, jsonProducts); // Writes it back to the Json File
        }


        // Delete Data Method 
        public bool DeleteData(string id)
        {
            // Get the current set, and remove the record with the specified ID from it
            var products = GetProducts().ToList();
            var productToDelete = products.FirstOrDefault(x => x.Id.Equals(id));

            // Null Check
            if (productToDelete != null)
            {
                products.Remove(productToDelete);
                SaveProducts(products);
                return true;
            }

            else
            {
                return false; //If the Product is NULL
            }
        }
    }
}