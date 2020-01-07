using PublicManager.DB.Entitys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PublicManager.Modules.Module_B.ProjectState.Forms
{
    public partial class StateEditForm : Form
    {
        private Project projectObj;
        public StateEditForm(Project proj)
        {
            InitializeComponent();

            projectObj = proj;
        }
    }
}