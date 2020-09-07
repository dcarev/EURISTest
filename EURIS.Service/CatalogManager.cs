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
    public class CatalogManager
    {
        LocalDbEntities context = new LocalDbEntities();

        /// <summary>
        /// Constructor
        /// </summary>
        public CatalogManager()
        {
            context = new LocalDbEntities();
        }

        /// <summary>
        /// Get all catalogs
        /// </summary>
        /// <returns>List of catalogs</returns>
        public List<Catalog> GetCatalogs()
        {
            List<Catalog> catalogs = new List<Catalog>();
            
            catalogs = (from item in context.Catalog
                        select item).ToList();

            return catalogs;
        }

        /// <summary>
        /// Finds catalog by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Catalog</returns>
        public Catalog FindCatalog(int Id)
        {
            var catalog = context.Catalog.FirstOrDefault(x => x.Id == Id);

            return catalog;
        }

        /// <summary>
        /// Find catalog by code
        /// </summary>
        /// <param name="code"></param>
        /// <returns>Catalog</returns>
        public Catalog FindCatalog(String code)
        {
            var catalog = context.Catalog.FirstOrDefault(x => x.Code == code);

            return catalog;
        }

        /// <summary>
        /// Checks for catalog matching id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>True if catalog exists</returns>
        public Boolean CatalogExists(int Id)
        {
            var catalog = FindCatalog(Id);

            if (catalog == null)
                return false;

            return true;
        }

        /// <summary>
        /// Checks for catalog matching code
        /// </summary>
        /// <param name="code"></param>
        /// <returns>True if catalog exists</returns>
        public Boolean CatalogExists(String code)
        {
            var catalog = FindCatalog(code);

            if (catalog == null)
                return false;

            return true;
        }

        /// <summary>
        /// Creates new catalog
        /// </summary>
        /// <param name="catalog"></param>
        /// <returns>Success if created, error message if not</returns>
        public ServiceResponse CreateCatalog(Catalog catalog)
        {
            if (CatalogExists(catalog.Code))
                return ServiceResponse.Error("Catalog with code " + catalog.Code + " already exists.");

            context.Catalog.Add(catalog);

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
        /// Updates catalog values
        /// </summary>
        /// <param name="catalog"></param>
        /// <returns>Success if updated, error message if not</returns>
        public ServiceResponse UpdateCatalog(Catalog catalog)
        {
            var updcatalog = FindCatalog(catalog.Id);
            updcatalog.Code = catalog.Code;
            updcatalog.Description = catalog.Description;

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
        /// Deletes catalog
        /// </summary>
        /// <param name="catalog"></param>
        /// <returns>>Success if deleted, error message if not</returns>
        public ServiceResponse DeleteCatalog(Catalog catalog)
        {
            context.Catalog.Remove(catalog);

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
