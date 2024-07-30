///////////////////////////////////////////////////////////////////
//
// Projects MINIMO
// Starter Kit 2024
//
// Youbiquitous Team
//
//

using Microsoft.AspNetCore.Mvc;
using Youbiquitous.Martlet.Core.Types;
using Youbiquitous.Martlet.Core.Types.Locale;
using Youbiquitous.Minimo.Settings.Core;

namespace Youbiquitous.Minimo.App.Controllers;

public partial class HintController : MinimoController
{
    public HintController(MinimoSettings settings)
        : base(settings)
    {
    }

    /// <summary>
    /// Returns matching countries for auto-completion purposes
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public JsonResult Countries([Bind(Prefix = "id")] string query = "")
    {
        var list = (from country in CountryRepository.AllBy(query)
            select new AutoCompleteItem
            {
                id = country.Ioc,
                value = country.CountryName
            }).ToList();

        return Json(list);
    }


}