using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FooMVCAppModern.Data;
using FooMVCAppModern.DTO;
using Microsoft.SqlServer.Management.Smo;

namespace FooMVCAppModern.Controllers
{
    public class FooController : Controller
    {
        // GET: Foo
        [HttpPost]
        public JsonResult ListAll(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            var total = new ObjectParameter("TotalCount", typeof(int));

            var sortConfig = (jtSorting ?? String.Empty).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string orderByColumn = null;
            string sortDirection = null;
            if (sortConfig.Length >= 1)
            {
                orderByColumn = sortConfig[0];
                if (sortConfig.Length >= 2)
                {
                    sortDirection = sortConfig[1];
                }
            }
            Database d;
            


           var res = new MetadataTestEntities().Foo_List(jtStartIndex, jtPageSize, orderByColumn, sortDirection, total);

            var allCol = res.ToArray();

            var payloadObj = new { Result = "OK", Records = allCol, TotalRecordCount = total.Value };
            var jsonResult = Json(payloadObj);
            return jsonResult;
        }

  
        [HttpPost]
        public JsonResult Update(FooDTO foo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }

                var res = new MetadataTestEntities().Foo_Update(foo.Id, foo.LastUpdate, foo.Active, foo.OtherFlag, foo.FKFirstNotNull, foo.FKSecondNull, foo.FKThridDefaultValue, foo.Labels);

                return Json(new { Result = res >= 0 ? "OK" : "ERROR" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var res = new MetadataTestEntities().Foo_Delete(id);
                return Json(new { Result = res > 0 ? "OK" : "ERROR" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

    }
}