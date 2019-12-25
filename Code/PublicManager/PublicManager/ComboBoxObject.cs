using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PublicManager
{
    /// <summary>
    /// ComboBoxObject
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ComboBoxObject<T>
    {
        public ComboBoxObject() { }

        public ComboBoxObject(string text, T tag)
        {
            this.Text = text;
            this.Tag = tag;
        }

        public string Text { get; set; }

        public T Tag { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}