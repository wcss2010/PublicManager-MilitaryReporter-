using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars.Ribbon;

namespace PublicManager.Modules
{
    public partial class MainMenuController : UserControl
    {
        public MainMenuController()
        {
            InitializeComponent();
        }

        public BackstageViewControl MenuControl
        {
            get { return bvcMenus; }
        }
    }
}