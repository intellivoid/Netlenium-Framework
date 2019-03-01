using Gecko;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netlenium.Driver.GeckoFXLib
{
    public class Element
    {
        private GeckoElement _Element;

        private Controller _DriverController;

        public Element(GeckoElement Element, Controller DriverController)
        {
            this._Element = Element;
            this._DriverController = DriverController;
        }

        public string Text
        {
            get
            {
                return this._Element.TextContent;
            }
            set
            {
                this._Element.TextContent = value;
            }
        }

        public string GetAttribute(string AttributeName)
        {
            return this._Element.GetAttribute(AttributeName);
        }

        public void SetAttribute(string AttributeName, string Value)
        {
            this._Element.SetAttribute(AttributeName, Value);
        }

        public List<Element> GetElements(Types.SearchType SearchType, string Input)
        {
            List<Element> Elements = new List<Element>();

            switch (SearchType)
            {
                case Types.SearchType.ClassName:

                    foreach (GeckoElement FoundElement in _Element.GetElementsByTagName(Input))
                    {
                        Elements.Add(new Element(FoundElement, this._DriverController));
                    }

                    return Elements;

                default:
                    throw new SearchTypeNotSupportedException();
            }
        }
    }
}
