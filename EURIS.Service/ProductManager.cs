using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EURIS.Entities;
using EURIS.Service;

namespace EURIS.Service
{
    public class ProductManager
    {
        LocalDbEntities context = new LocalDbEntities();

        /// <summary>
        /// Constructor
        /// </summary>
        public ProductManager()
        {
            context = new LocalDbEntities();
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>List of products</returns>
        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            
            products = (from item in context.Product
                        select item).ToList();

            return products;
        }

        /// <summary>
        /// Find product by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Product if find, null if not found</returns>
        public Product FindProduct(int Id)
        {
            var product = context.Product.FirstOrDefault(x => x.Id == Id);

            return product;
        }
        /// <summary>
        /// Find product by code
        /// </summary>
        /// <param name="code"></param>
        /// <returns>Product if find, null if not found</returns>
        public Product FindProduct(String code)
        {
            var product = context.Product.FirstOrDefault(x => x.Code == code);

            return product;
        }

        /// <summary>
        /// Checks for product matching id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>True if product exists</returns>
        public Boolean ProductExists(int Id)
        {
            var product = FindProduct(Id);

            if (product == null)
                return false;

            return true;
        }

        /// <summary>
        /// Checks for product matching code
        /// </summary>
        /// <param name="code"></param>
        /// <returns>True if product exists</returns>
        public Boolean ProductExists(String code)
        {
            var product = FindProduct(code);

            if (product == null)
                return false;

            return true;
        }

        /// <summary>
        /// Creates product in database
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Success if created, error message if not</returns>
        public ServiceResponse CreateProduct(Product product)
        {
            if (ProductExists(product.Code))
                return ServiceResponse.Error("Product with code " + product.Code + " already exists.");

            context.Product.Add(product);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                return ServiceResponse.Error(ex.Message);
            }

            return ServiceResponse.Success(ServiceMessages.Created);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public ServiceResponse UpdateProduct(Product product)
        {
            var updproduct = FindProduct(product.Id);
            updproduct.Code = product.Code;
            updproduct.Description = product.Description;

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                return ServiceResponse.Error(ex.Message);
            }

            return ServiceResponse.Success(ServiceMessages.Modified);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public ServiceResponse DeleteProduct(Product product)
        {
            context.Product.Remove(product);

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                return ServiceResponse.Error(ex.Message);
            }

            return ServiceResponse.Success(ServiceMessages.Deleted);
        }
    }
}
