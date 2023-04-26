using System;
using System.Text.Json;
using CoreMVC.UI.Models.Common;
using CoreMVC.UI.Models.VM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CoreMVC.UI.Controllers
{
	public class MyData
	{
		public string username { get; set; }
		public string pass { get; set; }
	}
	public class DefaultController : Controller
	{
		private readonly IConfiguration _configuration;
		public DefaultController(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public IActionResult Index()
		{
			var deger01 = _configuration.GetConnectionString("MyConn");
			var deger02 = _configuration["ConnectionStrings:MyConn"];
			var deger03 = _configuration["MyConn01"];
			var deger04 = _configuration["MyData:pass"];

			var deger05 = _configuration.GetSection("MyData").Get(typeof(MyData));

			MyData conf = (MyData)deger05;

			#region MyRegion

			//bu conf degiskenini tempdataya atmak istiyorum. 

			TempData.Add("hede", JsonSerializer.Serialize(conf));

			//attigim bu degeri cekmek istedigimde deserialize etmem gerekir. 

			var temptenGelenData = JsonSerializer.Deserialize<MyData>(TempData["hede"].ToString());

			#endregion

			Personel p = new Personel()
			{
				Ad="hüsamettin",
				Soyad = "cindoruk"
			};
			HttpContext.Session.SetString("bidi", JsonSerializer.Serialize(p));
			var userJson = HttpContext.Session.GetString("bidi");

			var myUser = JsonSerializer.Deserialize<Personel>(userJson);


			HttpContext.Session.MySet("zahide",p);
			var extensionValue = HttpContext.Session.MyGet<Personel>("zahide");


			//return View((conf,p));
			return View(Tuple.Create(conf,p));
		}
	}
}
