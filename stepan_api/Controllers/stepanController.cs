using stepan_api.Areas;
using stepan_api.Filters;
using stepan_api.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Description;

namespace stepan_api.Controllers
{

    [Authorize]
    [AntiForgeryTokenFilter]
    [RoutePrefix("api/stepan")]
        public class stepanController : ApiController
        {
               StepanEktron_oldEntities db = new StepanEktron_oldEntities();
               /// <summary>
               /// Get all Products according to all Categories.
               /// </summary>    
             [HttpGet]
               [Route("GetAllCategories")]
               public IHttpActionResult GetAllCategories()
               {
                   try
                   {
                       return Ok((db.ProductFinders.Select(x => new stepan_api.Models.ProductFinderWebsite { Name = x.Name, FormAt25C = x.FormAt25C, ViscosityAt200C = x.ViscosityAt200C }).OrderBy(x => x.Name)).ToList());
                   }
                   catch (Exception exp)
                   {
                       ExceptionLogging.SendExcepToDB(exp);
                       return BadRequest(exp.Message);
                   }
               }
        /// <summary>
        /// Get all Products according to parameter(category).
        /// </summary>
        /// <param name="category">The CategoryName of the Product.</param>
             [HttpGet]
              [Route("GetCategory")]
               public IHttpActionResult GetCategory(string category)
               {
                    try
                       {
                   if (category == null)
                   {
                       throw new ArgumentNullException("Please specify atleast one parameter");
                   }
                   else
                   {
                       var pf = ((db.ProductFinders.Where(y => y.Category == category).Select(x => new stepan_api.Models.ProductFinderWebsite { Name = x.Name, FormAt25C = x.FormAt25C, ViscosityAt200C = x.ViscosityAt200C }).OrderBy(x => x.Name)).ToList());
                           int check = pf.Count();
                           if (check == 0)
                           {
                               throw new System.ArgumentException("Product Not Found in Database according to Category Name");
                           }
                       return Ok(pf);
                   }
                       }
                       catch (Exception exp)
                       {
                           ExceptionLogging.SendExcepToDB(exp);
                           return BadRequest(exp.Message);
                       }
                }
             /// <summary>
             /// Get all Products according to all Brands.
             /// </summary>
            [HttpGet]
            [Route("GetAllBrandNames")]
            public IHttpActionResult GetAllBrandNames()
            {
              try
                 {
                     return Ok((db.ProductFinders.Select(x => new stepan_api.Models.ProductFinderWebsite { Name = x.Name, FormAt25C = x.FormAt25C, ViscosityAt200C = x.ViscosityAt200C }).OrderBy(x => x.Name)).ToList());
                }
                catch (Exception exp)
                {
                    ExceptionLogging.SendExcepToDB(exp);
                    return BadRequest(exp.Message);
                }
            }
            /// <summary>
            /// Get all Products according to parameter(brand).
            /// </summary>
            /// <param name="brand">The BrandName of the Product.</param>
         [HttpGet]
            [Route("GetBrandName")]
            public IHttpActionResult GetBrandName(string brand)
            {
                try
                    {
                if (brand == null)
                {
                    throw new ArgumentNullException("Please specify atleast one parameter");
                }
                else
                {
                    var pf = ((db.ProductFinders.Where(y => y.Brand == brand).Select(x => new stepan_api.Models.ProductFinderWebsite { Name = x.Name, FormAt25C = x.FormAt25C, ViscosityAt200C = x.ViscosityAt200C }).OrderBy(x => x.Name)).ToList());
                        int check = pf.Count();
                        if (check == 0)
                        {
                            throw new System.ArgumentException("Product Not Found in Database according to Brand Name");
                        }
                     return Ok(pf);
                }
                }
                    catch (Exception exp)
                    {
                        ExceptionLogging.SendExcepToDB(exp);
                        return BadRequest(exp.Message);
                    }
                
            }

         /// <summary>
         /// Get all Products according to all ChemicalGroups.
         /// </summary>
            [HttpGet]
            [Route("GetAllChemicalGroups")]
            public IHttpActionResult GetAllChemicalGroups()
            {
                try
                {
                    var pf = (from p in db.ProductFinders
                              join t in db.ProductTaxonomySummaries
                              on p.id equals t.item_id
                              select new stepan_api.Models.ProductFinderWebsite
                                 {
                                     Name = p.Name,
                                     FormAt25C = p.FormAt25C,
                                     ViscosityAt200C = p.ViscosityAt200C 
                                 }).OrderBy(p => p.Name).ToList();
                    return Ok(pf);
                }
                catch (Exception exp)
                {
                    ExceptionLogging.SendExcepToDB(exp);
                   return BadRequest(exp.Message);  
                    }
            }
            /// <summary>
            /// Get all Products according to parameter(ChemicalGroup).
            /// </summary>
            /// <param name="chemGroup">The ChemicalGroup of the Product.</param>
         [HttpGet]
         [Route("GetChemicalGroup")]
            public IHttpActionResult GetChemicalGroup(string chemGroup)
            {
                try
                    {
                        if (chemGroup == null)
                {
                    throw new ArgumentNullException("Please specify atleast one parameter");
                }
                else
                {
                        var pf = (from x in db.ProductFinders
                                  join y in db.ProductTaxonomySummaries
                                  on x.id equals y.item_id
                                  where (y.chemGroups.Contains(chemGroup))
                                  select new stepan_api.Models.ProductFinderWebsite
                                  {
                                      Name = x.Name,
                                      FormAt25C = x.FormAt25C,
                                      ViscosityAt200C = x.ViscosityAt200C
                                  }).OrderBy(x => x.Name).ToList();
                        int check = pf.Count();
                        if (check == 0)
                        {
                            throw new System.ArgumentException("Product Not Found in Database according to Chemical Description");
                        }
                            return Ok(pf);
                    }
                }
                    catch (Exception exp)
                    {
                        ExceptionLogging.SendExcepToDB(exp);
                        return BadRequest(exp.Message);
                    }
            }
         /// <summary>
         /// Get all Products according to all ChemicalName.
         /// </summary>
             [HttpGet]
            [Route("GetAllChemicalNames")]
            public IHttpActionResult GetAllChemicalNames()
            {
                try
                {
                    var pf = (from a in db.ProductFinders
                              join b in db.ProductFinderLinks on a.id equals b.ProductID
                              join c in db.ProductFinderTaxonomies on
                              b.TaxonomyID equals c.id
                              select new
                              stepan_api.Models.ProductFinderWebsite
                              {
                                  Name = a.Name,
                                  FormAt25C = a.FormAt25C,
                                  ViscosityAt200C = a.ViscosityAt200C 
                              }).OrderBy(a => a.Name).ToList();
                    return Ok(pf);
                }
                catch (Exception exp)
                {
                    ExceptionLogging.SendExcepToDB(exp);
                    return BadRequest(exp.Message);
                }
            }
             /// <summary>
             /// Get all Products according to parameter(ChemicalName).
             /// </summary>
             /// <param name="name">The ChemicalName of the Product.</param>
         [HttpGet]
            [Route("GetChemicalName")]
            public IHttpActionResult GetChemicalName(string name)
            { try
                    {
                if (name == null)
                {
                    throw new ArgumentNullException("Please specify atleast one parameter");
                }
                else
                {
                        var pf = (from a in db.ProductFinders
                                  join b in db.ProductFinderLinks on
                                  a.id equals b.ProductID
                                  join c in db.ProductFinderTaxonomies
                                  on b.TaxonomyID equals c.id
                                  where c.Name == name
                                  select new
                                    stepan_api.Models.ProductFinderWebsite
                                    {
                                        Name = a.Name,
                                        FormAt25C = a.FormAt25C,
                                        ViscosityAt200C = a.ViscosityAt200C
                                    }).OrderBy(a => a.Name).ToList();
                        int check = pf.Count();
                        if (check == 0)
                        {
                            throw new System.ArgumentException("Product Not Found in Database according to Chemical Name ");
                        }
                              return Ok(pf);
                    }
            }
                  catch (Exception exp)
                    {
                        ExceptionLogging.SendExcepToDB(exp);
                        return BadRequest(exp.Message);
                    }
                
            }
         /// <summary>
         /// Get all Products according to parameter(Search Parameter).
         /// </summary>
         /// <param name="query">The SearchParameter(ProductName,BrandName,CategoryName,ChemicalDescription) of the Product.</param>
                [HttpGet]
            [Route("GetSearchResult")]
            public IHttpActionResult GetSearchResult(string query)
            {
                 try
                    {
                if (query == null)
                {
                    throw new ArgumentNullException("Please specify atleast one parameter");
                }
                else
                {
                    //join b in db.ProductFinderLinks on
                                      //a.id equals b.ProductID
                        var pf = (from a in db.ProductFinders
                                      where a.Name == query || a.Brand == query || a.Category == query || a.ChemicalDescription == query
                                      select new
                                            stepan_api.Models.ProductFinderWebsite
                                          {
                                              Name = a.Name,
                                              FormAt25C = a.FormAt25C,
                                              ViscosityAt200C = a.ViscosityAt200C
                                          }).OrderBy(a => a.Name).ToList();
                        int check = pf.Count();
                        if (check == 0)
                        {
                            throw new System.ArgumentException("Product Not Found in Database according to search parameter");
                        }
                            return Ok(pf);
                        }
                 }
                    catch (Exception exp)
                    {
                        ExceptionLogging.SendExcepToDB(exp);
                        return BadRequest(exp.Message);
                    }
            }
                /// <summary>
                /// Get all Brands according to parameter(category).
                /// </summary>
                /// <param name="category">The Category of the Product.</param>
            [HttpGet]
            [Route("GetSubCategoryOfCategory")]
            public IHttpActionResult GetSubCategoryOfCategory(string category)
            {
                try { 
                if (category == null)
                {
                    throw new ArgumentNullException("Please specify atleast one parameter");
                }
                else
                {
                    var pf = (from a in db.ProductFinders
                              where a.Category == category
                              select a.Brand).ToList();
                    int check = pf.Count();
                    if (check == 0)
                    {
                        throw new System.ArgumentException("Product Not Found in Database according to Category Name ");
                    }
                    return Ok(pf);
                }
                }
                catch(Exception exp)
                    {
                        ExceptionLogging.SendExcepToDB(exp);
                        return BadRequest(exp.Message);
                    }
            }
            /// <summary>
            /// Get all Description of Products according to parameter(brand).
            /// </summary>
            /// <param name="brand">The BrandName of the Product.</param>
      [HttpGet]
        [Route("GetDescriptionOfSubCategory")]
            public IHttpActionResult GetDescriptionOfSubCategory(string brand)
            {
                try
                {
                    if (brand == null)
                    {
                        throw new ArgumentNullException("Please specify atleast one parameter");
                    }
                    else
                    {
                        var pf = (from a in db.ProductFinders
                                  where a.Brand == brand
                                  select new stepan_api.Models.ProductFinderWithAllParameter
                                  {
                                      Name = a.Name,
                                      FormAt25C = a.FormAt25C,
                                      ViscosityAt200C = a.ViscosityAt200C,
                                      ViscosityAt25C = a.ViscosityAt25C,
                                      ViscosityAtC = a.ViscosityAtC,
                                      AcidNumber = a.AcidNumber,
                                      AcidValue = a.AcidValue,
                                      ApproxTgC = a.ApproxTgC,
                                      CloudPoint = a.CloudPoint,
                                      CMC = a.CMC,
                                      Density = a.Density,
                                      DravesWetting = a.DravesWetting,
                                      FlashPoint = a.FlashPoint,
                                      FoamDensity = a.FoamDensity,
                                      FreeFattyAcid = a.FreeFattyAcid,
                                      FreezePoint = a.FreezePoint,
                                      HLB = a.HLB,
                                      HydroxylValue = a.HydroxylValue,
                                      InsulationValue = a.InsulationValue,
                                      IntrafacialTension = a.IntrafacialTension,
                                      Kosher = a.Kosher,
                                      MolesOfEO = a.MolesOfEO,
                                      MolesOfPO = a.MolesOfPO,
                                      OH_Functionality = a.OH_Functionality,
                                      PercentActive = a.PercentActive,
                                      PourPoint = a.PourPoint,
                                      Solids = a.Solids,
                                      SpecificGravity = a.SpecificGravity,
                                      SurfaceTension = a.SurfaceTension,
                                      ThermalStability = a.ThermalStability,
                                      Triglycerides = a.Triglycerides,
                                      VOC = a.VOC
                                  }).ToList();
                        int check = pf.Count();
                        if (check == 0)
                        {
                            throw new System.ArgumentException("Product Not Found in Database according to Brand Name");
                        }

                        return Ok(pf);
                    }
                }
                catch (Exception exp)
                {
                    ExceptionLogging.SendExcepToDB(exp);
                    return BadRequest(exp.Message);
                }
            }
      /// <summary>
      /// Get all Information of Products according to HashTags(parameter).
      /// </summary>
      /// <param name="parameter">The HashTags of the Product.</param>
      [HttpGet]
      [Route("GetData")]
      public IHttpActionResult GetData(string parameter)
      {
          string markets="";
          string chemicalGroups = "";
          string name ="";
          string[] data = parameter.Split('_');
          //for (int i = 0; i < data.Count(); i++)
          //{ }
          if (data.Count() == 1)
          {
              markets = data[0].ToString();
          }
          else if (data.Count() == 2)
          {
              markets = data[0].ToString();
              chemicalGroups = data[1].ToString();
          }
          else if (data.Count() == 3)
          {
              markets = data[0].ToString();
              chemicalGroups = data[1].ToString();
              name = data[2].ToString();
          }
          else
          {
              markets = data[0].ToString();
              chemicalGroups = data[1].ToString();
              name = data[2].ToString();
          }

         try
          {
              if (parameter == null)
              {
                  throw new ArgumentNullException("Please specify atleast one parameter");
              }
              else
              {
                  var pf = (from a in db.ProductFinders
                            join b in db.ProductTaxonomySummaries
                            on a.id equals b.item_id
                            where b.markets.StartsWith(markets) && b.markets.EndsWith(markets) || b.chemGroups.StartsWith(chemicalGroups) && b.chemGroups.EndsWith(chemicalGroups) || a.Name == name
                            select new stepan_api.Models.ProductFinderCatalog
                            {
                                Name = a.Name,
                                ChemicalDescription = a.ChemicalDescription,
                                PercentActive = a.PercentActive,
                                FormAt25C = a.FormAt25C,
                                Application = b.applications
                            }).ToList();
                  int check = pf.Count();
                  if (check == 0)
                  {
                      throw new System.ArgumentException("Product Not Found in Database according to Category Name ");
                  }
                  return Ok(pf);
              }
          }
          catch (Exception exp)
          {
              ExceptionLogging.SendExcepToDB(exp);
              return BadRequest(exp.Message);
          }
      }
    }
}
