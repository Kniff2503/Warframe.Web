// ###############################################################
// Thomas Heise
// Warframe.Web
// ItemViewModel.cs
// 2020/01/05/14:56
// ###############################################################

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Warframe.Web.Models
{
    public class ItemViewModel
    { 
        public string Value { get; set; }
        public List<SelectListItem> Items { get; set; }
    }
}