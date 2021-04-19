using Dame_2.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Dame_2.Models
{
    class Square : BaseNotification
    {
        public Square(int line, int column, Type type)
        {
            this.Line = line;
            this.Column = column;
            this.Type = type;
        }

        private int line;
        public int Line
        {
            get { return line; }
            set
            {
                line = value;
                NotifyPropertyChanged("line");
            }
        }
        private int column;
        public int Column
        {
            get { return column; }
            set
            {
                column = value;
                NotifyPropertyChanged("column");
            }
        }

        private string image;
        public string Image
        {
            get { return image; }
            set
            {
                image = value;
                NotifyPropertyChanged("Image");
            }
        }

        private Type type;

        public Type Type
        {
            get { return type; }
            set { Image = Watch.GetPath(value); type = value; }
        }

    }
}

