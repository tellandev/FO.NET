//Apache2, 2019, daniiiol
//Apache2, 2017, WinterDev
//Apache2, 2009, griffm, FO.NET
using Fonet;

namespace FonetExample
{
    class HelloWorld
    {
        static void Main(string[] args)
        {
            FonetDriver driver = FonetDriver.Make();
            driver.Render("hello.fo", "hello.pdf");
        }
    }
}