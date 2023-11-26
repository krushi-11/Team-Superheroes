using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using SuperHeroes.WebSite.Models;
using Microsoft.AspNetCore.Hosting;

namespace SuperHeroes.WebSite.Services
{
   /// <summary>
   /// Service for managing product data stored in JSON file
   /// </summary>
    public class JsonFileProductService
    {
        /// <summary>
        /// Constructor for JsonFileProductService
        /// </summary>
        /// <param name="webHostEnvironment"></param>
        public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// Gets the hosting environment
        /// </summary>
        public IWebHostEnvironment WebHostEnvironment { get; }

        /// <summary>
        /// Gets the path to the Json File
        /// </summary>
        /// <returns>It returns the Path to combine Web Environment</returns>
        private string JsonFileName
        {
            get
            {
                // Path to Json File
                return Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json"); 
            }
        }

        /// <summary>
        /// Get all Products from Json File
        /// </summary>
        /// <returns>It returns the serialized data in Json File</returns>
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


        /// <summary>
        /// Update Data Method
        /// </summary>
        /// <param name="updatedProduct"></param>
        /// <returns>It returns the data of the product</returns>
        public ProductModel UpdateData(ProductModel updatedProduct)
        {
            // Get the Products
            var products = GetProducts();
            // Filter by id
            var productData = products.FirstOrDefault(x=>x.Id.Equals(updatedProduct.Id));

            // Set Old Title to Updated Title
            productData.Title = updatedProduct.Title;
            // Set Old Description to Updated Description
            productData.Description = updatedProduct.Description;
            // Set Old Url to Updated Url
            productData.Url = updatedProduct.Url;
            // Set Old Image to Updated Image
            productData.Image = updatedProduct.Image;
            // Set Old Price to Updated Price
            productData.Price = updatedProduct.Price;
            // Set Old Stock to Updated Stock
            productData.Stock = updatedProduct.Stock;

            // Save the Updated List
            SaveProducts(products); 

            return productData;
        }


        /// <summary>
        /// Create Data Method
        /// This method creates a new product and saves it in the Json File
        /// </summary>
        /// <param name="productModel"></param>
        /// <returns>It returns the productModel</returns>
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


        /// <summary>
        /// Save product after converting it to json format
        /// </summary>
        /// <param name="products"></param>
        private void SaveProducts(IEnumerable<ProductModel> products)
        {
            var jsonProducts = JsonSerializer.Serialize(products,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });

            // Writes it back to the Json File
            File.WriteAllText(JsonFileName, jsonProducts); 
        }


        /// <summary>
        /// Delete Data Method
        /// This method is used to remove the existing product from the Json File
        /// </summary>
        /// <param name="id"></param>
        /// <returns>If the product is valid, it deletes the product from the Json File 
        /// else it will return false </returns>
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
                //If the Product is NULL
                return false; 
            }
        }
    }
}