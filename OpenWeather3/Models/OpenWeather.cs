using OpenWeather3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Openweather3.Models
{
	class OpenWeather
	{
		
		public List<Weather> weather { get; set; }
		public Main main { get; set; }
		
	}
}