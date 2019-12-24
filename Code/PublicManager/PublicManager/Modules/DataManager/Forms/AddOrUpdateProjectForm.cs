using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PublicManager.Modules.Manager.Forms
{
    public partial class AddOrUpdateProjectForm : Form
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName
        {
            get
            {
                return txtProjectName.Text;
            }

            set
            {
                txtProjectName.Text = value;
            }
        }

        /// <summary>
        /// 研究周期
        /// </summary>
        public ComboBoxObject<int> StudyTime
        {
            get
            {
                return (ComboBoxObject<int>)txtStudyTime.SelectedItem;
            }

            set
            {
                if (value != null && value is ComboBoxObject<int>)
                {
                    ComboBoxObject<int> valObj = ((ComboBoxObject<int>)value);
                    foreach (ComboBoxObject<int> item in txtStudyTime.Items)
                    {
                        if (item.Text == valObj.Text && item.Tag == valObj.Tag)
                        {
                            txtStudyTime.SelectedItem = item;
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 研究经费
        /// </summary>
        public string StudyMoney
        {
            get
            {
                return txtStudyMoney.Text;
            }

            set
            {
                txtStudyMoney.Text = value;
            }
        }

        /// <summary>
        /// 负责单位
        /// </summary>
        public string DutyUnit
        {
            get
            {
                return txtDutyUnit.Text;
            }

            set
            {
                txtDutyUnit.Text = value;
            }
        }

        /// <summary>
        /// 研究周期选项
        /// </summary>
        public ComboBox.ObjectCollection StudyTimeItems
        {
            get
            {
                return txtStudyTime.Items;
            }
        }

        public AddOrUpdateProjectForm()
        {
            InitializeComponent();

            loadComboboxItems();
        }

        /// <summary>
        /// 动态加载各种选项
        /// </summary>
        private void loadComboboxItems()
        {
            //加载研究周期选项
            if (MainConfig.Config.ObjectDict.ContainsKey("研究周期"))
            {
                try
                {
                    txtStudyTime.Items.Clear();
                    Newtonsoft.Json.Linq.JArray teams = (Newtonsoft.Json.Linq.JArray)MainConfig.Config.ObjectDict["研究周期"];
                    foreach (string ssss in teams)
                    {
                        string[] ttt = ssss.Split(new string[] { MainConfig.rowFlag }, StringSplitOptions.None);
                        if (ttt != null && ttt.Length >= 2)
                        {
                            ComboBoxObject<int> comboboxObject = new ComboBoxObject<int>();
                            comboboxObject.Text = ttt[0];
                            comboboxObject.Tag = int.Parse(ttt[1]);
                            txtStudyTime.Items.Add(comboboxObject);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.ToString());
                }
            }
            //加载责任单位选项
            if (MainConfig.Config.ObjectDict.ContainsKey("责任单位"))
            {
                try
                {
                    txtDutyUnit.Items.Clear();
                    Newtonsoft.Json.Linq.JArray teams = (Newtonsoft.Json.Linq.JArray)MainConfig.Config.ObjectDict["责任单位"];
                    foreach (string s in teams)
                    {
                        txtDutyUnit.Items.Add(s);
                    }
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.ToString());
                }
            }            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ProjectName))
            {
                MessageBox.Show("对不起，请输入项目名称！");
                return;
            }
            if (StudyTime == null)
            {
                MessageBox.Show("对不起，请输入研究周期！");
                return;
            }
            if (string.IsNullOrEmpty(StudyMoney))
            {
                MessageBox.Show("对不起，请输入研究经费！");
                return;
            }
            Single dd21;
            if (Single.TryParse(StudyMoney, out dd21) == false)
            {
                MessageBox.Show("对不起，请输入正确的研究经费!");
                return;
            }
            if (string.IsNullOrEmpty(DutyUnit))
            {
                MessageBox.Show("对不起，请输入责任单位！");
                return;
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }

    /// <summary>
    /// ComboBoxObject
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ComboBoxObject<T>
    {
        public string Text { get; set; }

        public T Tag { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}