using System;
using System.Collections.Generic;
using System.Text;

namespace RestoramaConsole
{
    public enum ProductType
    {
        Bread,
        Cutlet,
        Tomato,
        Cheese,
        Lettuce
    }
    public class ProductItem
    {
        public ProductItem(ProductType type)
        {
            Type = type;
        }

        public ProductType Type { get; private set; }
    }
}
