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

        //Add Ratings Method
        public bool AddRating(string productId, int rating)
        {
            var products = GetProducts();

            if (string.IsNullOrEmpty(productId))
            {
                // Handle the case where productId is null or empty (you can throw an exception, log, or return false)
                // For example, return false to indicate failure:
                return false;
            }

            var product = products.FirstOrDefault(x => x.Id == productId);
            if (product != null)
            {
                if (product.Ratings == null)
                {
                    product.Ratings = new int[] { rating };
                }
                else
                {
                    var ratings = product.Ratings.ToList();
                    ratings.Add(rating);
                    product.Ratings = ratings.ToArray();
                }

                using (var outputStream = File.OpenWrite(JsonFileName))
                {
                    JsonSerializer.Serialize<IEnumerable<ProductModel>>(
                        new Utf8JsonWriter(outputStream, new JsonWriterOptions
                        {
                            SkipValidation = true,
                            Indented = true
                        }),
                        products
                    );
                }

                // Return true to indicate success
                return true;
            }
            else
            {
                // Handle the case where no product with the given productId is found (throw an exception, log, or return false)
                // For example, return false to indicate failure:
                return false;
            }
        }

        // Update Data Method
        public ProductModel UpdateData(ProductModel updatedProduct)
        {
            if(updatedProduct!= null)
            {
                var products = GetProducts().ToList();
                var productData = products.FirstOrDefault(x=>x.Id.Equals(updatedProduct.Id));

                productData.Title = updatedProduct.Title;
                productData.Description = updatedProduct.Description;
                productData.Url = updatedProduct.Url;
                productData.Image = updatedProduct.Image;

                SaveProductsToJson(products);
                return productData;
            }
            else 
            {
                System.Console.WriteLine("Data is null");
                return null; 
            }
        }

        private void SaveProductsToJson(List<ProductModel> products)
        {
            using (var outputStream = File.OpenWrite(JsonFileName))
                {
                    JsonSerializer.Serialize<IEnumerable<ProductModel>>(
                        new Utf8JsonWriter(outputStream, new JsonWriterOptions
                        {
                            SkipValidation = true,
                            Indented = true
                        }),
                        products
                    );
                }
        }
    }
}