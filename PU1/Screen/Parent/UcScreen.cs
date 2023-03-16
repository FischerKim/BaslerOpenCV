using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PU1
{
    public partial class UcScreen : UserControl
    {
        #region DELEGATE & EVENT
        public delegate void ScreenFixedHandler(bool BFixed);
        public event ScreenFixedHandler ScreenFixed = null;
        #endregion


        #region CONSTRUCTOR & DESTRUCTOR
        public UcScreen()
        {
            InitializeComponent();
        }
        #endregion


        #region FUNCTION
        public virtual void Add() { }


        public virtual void Remove() { }


        public virtual void NotifyRecipeChanged() { }


        protected void OnScreenFixed(bool BFixed)
        {
            try
            {
                if (this.ScreenFixed != null)
                {
                    this.ScreenFixed(BFixed);
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion
    }
}

