using System.Collections.Generic;
using System.Web.Mvc;

namespace DataClassLibrary.Models
{
    public class Categories
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }

        public IEnumerable<SelectListItem> CategoriesList
        {
            get
            {
                return
                    new SelectListItem[]
                    {   
                        new SelectListItem { Value = "Books", Text = "Books"},
                        new SelectListItem { Value = "Mobiles", Text = "Mobiles"},
                        new SelectListItem { Value = "Computers", Text = "Computers"},
                        new SelectListItem { Value = "Cameras", Text = "Cameras"},
                        new SelectListItem { Value = "VideoPlayers", Text = "VideoPlayers"},
                    };
            }
        }

    }
}
