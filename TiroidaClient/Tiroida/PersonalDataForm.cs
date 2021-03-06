﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MetroFramework.Controls;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Tiroida
{
    public partial class PersonalDataForm : UserControl
    {
        delegate void SetGitAndEnableStatusCallBack(bool gifstatus, bool enablestatus);
        delegate void SetInterfaceCallBack(string chanse, string chanse_to_have_nothing);
        delegate string GetInfoCallBack(MetroComboBox combobox);
        private List<string> unknownbox;

        private string GetInfoText(MetroComboBox combobox)
        {
            if (combobox.InvokeRequired)
            {
                GetInfoCallBack callback = new GetInfoCallBack(GetInfoText);
                return (string)this.Invoke(callback, new object[] { combobox });
            }
            else
            {
                return combobox.Text;
            }
        }

        private void SetResponseInterface(string chanse, string chanse_to_have_nothing)
        {

            if (this.InvokeRequired)
            {
                SetInterfaceCallBack callback = new SetInterfaceCallBack(SetResponseInterface);
                this.Invoke(callback, new object[] { chanse, chanse_to_have_nothing });
            }
            else
            {
                Application.UseWaitCursor = false;
                ResponseUserControl usercontrol = new ResponseUserControl();
                usercontrol.SetCancer(chanse);
                usercontrol.SetNonCancer(chanse_to_have_nothing);
                usercontrol.SetAnimateCancer((int)double.Parse(chanse_to_have_nothing));


                Panel panel1 = (Panel)this.Parent;
                panel1.Controls.Clear();
                panel1.Controls.Add(usercontrol);
            }
        }

        private void SetGif(bool gifstatus, bool enablestatus)
        {
            if (this.metroButton1.InvokeRequired)
            {
                SetGitAndEnableStatusCallBack callback = new SetGitAndEnableStatusCallBack(SetGif);
                this.Invoke(callback, new object[] { gifstatus, enablestatus });
            }
            else
            {
                
                this.metroButton1.Enabled = enablestatus;
            }
        }

        public PersonalDataForm(bool loged)
        {
            InitializeComponent();
            SetPanelLanguage();
            if (loged)
            {
                pictureBox2.Image = new Bitmap(Properties.Resources.logout);
            }
            else
            {
                this.metroButton2.Enabled = false;
            }

            // AddToList("metroComboBox16", "metroComboBox17", "metroComboBox15", "metroComboBox14", "metroComboBox13");
            
        }


        private void AddToList(params string[] list)
        {
            for (int i = 0; i < list.Length; i++)
            {
                unknownbox.Add(list[i]);
            }
        }

        private void changeLanguageCombobox(MetroComboBox box, string yes, string no, string unknown, bool isunknown)
        {
            box.Items.Clear();
            box.Items.Add(yes);
            box.Items.Add(no);
            if (isunknown)
            {
                box.Items.Add(unknown);
            }

        }



        private void changeComboBoxlang(Control control)
        {

            languagesettings ls = ConnectionClass.languagesupporter.getLanguagesettings();
            if (control is MetroComboBox)
            {
                MetroComboBox box = (MetroComboBox)control;

                if (box != null)
                {
                    this.unknownbox = new List<string> { "metroComboBox16", "metroComboBox17", "metroComboBox15", "metroComboBox14", "metroComboBox13" };

                    if (unknownbox.Contains(box.Name))
                    {
                        changeLanguageCombobox(box, ls.yes_answer, ls.no_answer, ls.unknown_answer, true);
                    }
                    else
                    {
                        changeLanguageCombobox(box, ls.yes_answer, ls.no_answer, ls.unknown_answer, false);
                    }
                }

            }
            else
                foreach (Control child in control.Controls)
                {
                    changeComboBoxlang(child);
                }
        }


        public void ReloadLanguage()
        {
            languagesettings ls = ConnectionClass.languagesupporter.getLanguagesettings();
            this.metroLabel1.Text = ls.sex;
            this.metroLabel2.Text = ls.age;
            this.metroLabel3.Text = ls.on_thyroxine;
            this.metroLabel4.Text = ls.query_on_thyroxine;
            this.metroLabel5.Text = ls.on_antithyroid_medication;
            this.metroLabel11.Text = ls.tumor;
            this.metroLabel20.Text = ls.FTI_measured;
            this.metroLabel21.Text = ls.FTI;
            this.metroLabel6.Text = ls.thyroid_surgery;
            this.metroLabel8.Text = ls.query_hypothyroid;
            this.metroLabel7.Text = ls.query_hyperthyroid;
            this.metroLabel9.Text = ls.pregnant;
            this.metroLabel10.Text = ls.sick;
            this.metroLabel12.Text = ls.lithium;
            this.metroLabel22.Text = ls.TBG_measured;
            this.metroLabel23.Text = ls.TBG;
            this.metroLabel13.Text = ls.goitre;
            this.metroLabel14.Text = ls.TSH_measured;
            this.metroLabel15.Text = ls.TSH;
            this.metroLabel16.Text = ls.T3_measured;
            this.metroLabel17.Text = ls.T3;
            this.metroLabel18.Text = ls.TT4_measured;
            this.metroLabel19.Text = ls.TT4;
            this.metroLabel24.Text = ls.patient_name;
            this.metroButton2.Text = ls.result;
            this.metroButton1.Text = ls.send_data;
            this.metroButton3.Text = ls.verify_photo;

            changeComboBoxlang(this);
            /*
            foreach (MetroComboBox box in this.Controls.OfType<MetroComboBox>())
            {
                Console.WriteLine("yes");
                if (unknownbox.Contains(box.Name))
                {
                    changeLanguageCombobox(box, ls.yes_answer, ls.no_answer, ls.unknown_answer, true);
                }
                else
                {
                    changeLanguageCombobox(box,ls.yes_answer,ls.no_answer,ls.unknown_answer,false);
                }
            }

            
            changeLanguageCombobox(this.metroComboBox2,ls.yes_answer,ls.no_answer,ls.unknown_answer,false);
            changeLanguageCombobox(this.metroComboBox3, ls.yes_answer, ls.no_answer, ls.unknown_answer, false);
            changeLanguageCombobox(this.metroComboBox10, ls.yes_answer, ls.no_answer, ls.unknown_answer, false);
            changeLanguageCombobox(this.metroComboBox16, ls.yes_answer, ls.no_answer, ls.unknown_answer, true);
            changeLanguageCombobox(this.metroComboBox5, ls.yes_answer, ls.no_answer, ls.unknown_answer, false);
            changeLanguageCombobox(this.metroComboBox6, ls.yes_answer, ls.no_answer, ls.unknown_answer, false);
            changeLanguageCombobox(this.metroComboBox7, ls.yes_answer, ls.no_answer, ls.unknown_answer, false);
            changeLanguageCombobox(this.metroComboBox8, ls.yes_answer, ls.no_answer, ls.unknown_answer, false);
            changeLanguageCombobox(this.metroComboBox9, ls.yes_answer, ls.no_answer, ls.unknown_answer, false);
            changeLanguageCombobox(this.metroComboBox9, ls.yes_answer, ls.no_answer, ls.unknown_answer, false);
            changeLanguageCombobox(this.metroComboBox9, ls.yes_answer, ls.no_answer, ls.unknown_answer, false);
            changeLanguageCombobox(this.metroComboBox9, ls.yes_answer, ls.no_answer, ls.unknown_answer, false);
            changeLanguageCombobox(this.metroComboBox9, ls.yes_answer, ls.no_answer, ls.unknown_answer, false);
            changeLanguageCombobox(this.metroComboBox9, ls.yes_answer, ls.no_answer, ls.unknown_answer, false);
            changeLanguageCombobox(this.metroComboBox9, ls.yes_answer, ls.no_answer, ls.unknown_answer, false);
            */
        }


        private void SetPanelLanguage()
        {
            if (ConnectionClass.config.Language != "Romanian")
            {
                ReloadLanguage();
            }

        }





        private void PersoanlDataForm_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            //pictureBox2.Image = new Bitmap(Properties.Resources.logout);
        }

        private void metroTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            
            this.metroButton1.Enabled = false;
            this.metroButton2.Enabled = false;
            this.pictureBox2.Enabled = false;
            Application.UseWaitCursor = true;
            Thread th = new Thread(SendPersonalData);
            th.Start();
            //SendPersonalData();
            
        }

        private string GetSex()
        {
            string sex = GetInfoText(this.metroComboBox1);
            switch (sex)
            {
                case "Masculin":
                    return "M";
                    break;
                case "Feminin":
                    return "F";
                    break;
                default:
                    return "?";
                    break;
            }
        }

        private string GetOption(MetroComboBox combobox)
        {
            string selectedvalue = GetInfoText(combobox);
            switch (selectedvalue)
            {
                case "Da":
                    return "t";
                    break;
                case "Nu":
                    return "f";
                    break;
                default:
                    return "f";
                    break;
            }
        }

        private string GetValue(NumericUpDown numeric)
        {
            if (numeric.Enabled)
            {
                return numeric.Value.ToString();
            }
            else
            {
                return "?";
            }
        }

        private string GetAge()
        {
            return numericUpDown1.Value.ToString();
        }

        private string GetPregnantStatus()
        {
            if (!this.metroComboBox8.Enabled)
                return "f";

            switch (this.metroComboBox8.Text)
            {
                case "Da":
                    return "t";
                    break;
                case "Nu":
                    return "f";
                    break;
                default:
                    return "f";
                    break;
            }
        }

        private void SendPersonalData()
        {
            
            if (ConnectionClass.ClientTCP == null)
            {
                SetGif(false, true);
                MessageBox.Show("Sunteti momentan offline","Tiroida");
                return;
            }
            
            

            PersonalData data = new PersonalData();

            data.action = "analize";
            data.Age = GetAge();
            data.Sex = GetSex();
            data.on_thyroxine = GetOption(this.metroComboBox2);
            data.query_on_thyroxine = GetOption(this.metroComboBox3);
            data.on_antithyroid_medication = GetOption(this.metroComboBox4);
            data.thyroid_surgery = GetOption(this.metroComboBox5);
            data.query_hypothyroid = GetOption(this.metroComboBox6);
            data.query_hyperthyroid = GetOption(this.metroComboBox7);
            data.pregnant = GetOption(this.metroComboBox8);
            data.sick = GetOption(this.metroComboBox9);
            data.tumor = GetOption(this.metroComboBox10);
            data.lithium = GetOption(this.metroComboBox11);
            data.goitre = GetOption(this.metroComboBox12);
            data.TSH_measured = GetOption(this.metroComboBox13);
            data.TSH = GetValue(this.numericUpDown2);
            data.T3_measured = GetOption(this.metroComboBox14);
            data.T3 = GetValue(this.numericUpDown3);
            data.TT4_measured = GetOption(this.metroComboBox15);
            data.TT4 = GetValue(this.numericUpDown4);
            data.FTI_measured = GetOption(this.metroComboBox16);
            data.FTI = GetValue(this.numericUpDown5);
            data.TBG_measured = GetOption(this.metroComboBox17);
            data.TBG = GetValue(this.numericUpDown6);
            data.patient_name = this.metroTextBox1.Text;
            data.cookie = ConnectionClass.ClientTCP.Cookie;

            

            string datatosend = JsonConvert.SerializeObject(data);
            
            if (ConnectionClass.ClientTCP != null)
            {
                ConnectionClass.ClientTCP.SendContent(datatosend);
                ConnectionClass.ClientTCP.OnResponse += ClientTCP_OnResponse;
            }
        }

        private void ClientTCP_OnResponse(object sender, OnReceiveMessageClientEventArgs e)
        {
            
            SetGif(false, true);
            SetResponseInterface((e.Medical.Chanse_To_Have*100).ToString(), (e.Medical.Chanse_To_Have_Nothing*100).ToString());
            ConnectionClass.ClientTCP.OnResponse -= ClientTCP_OnResponse;
        }

        private void ChangeValueEvent(MetroComboBox combobox, NumericUpDown numeric)
        {
            if (combobox.Text == "Nu" || combobox.Text == "Necunoscut")
            {
                numeric.Enabled = false;
            }
            else
            {
                if (combobox.Text == "Da")
                {
                    numeric.Enabled = true;
                }
            }
        }

        private void metroComboBox16_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeValueEvent(this.metroComboBox16, this.numericUpDown5);
        }

        private void metroComboBox17_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeValueEvent(this.metroComboBox17, this.numericUpDown6);
        }

        private void metroComboBox13_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeValueEvent(this.metroComboBox13, this.numericUpDown2);
        }

        private void metroComboBox14_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeValueEvent(this.metroComboBox14,this.numericUpDown3);
        }

        private void metroComboBox15_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeValueEvent(metroComboBox15, numericUpDown4);
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (metroComboBox1.Text == "Masculin" || metroComboBox1.Text == "Necunoscut")
                metroComboBox8.Enabled = false;
            else
                metroComboBox8.Enabled = true;
        }

        private void RemoveCookie()
        {

            cookieobj cookie = new cookieobj("");
            string cookiesave = JsonConvert.SerializeObject(cookie);
            Console.WriteLine(cookiesave);
            System.IO.StreamWriter writer = new System.IO.StreamWriter(@"cookie.json",false);
            writer.Write(cookiesave);
            writer.Close();
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            RemoveCookie();



            Login lg = new Login();
            Panel panel1 = (Panel)this.Parent;
            panel1.Controls.Clear();
            panel1.Controls.Add(lg);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }




        private void metroLabel1_Click(object sender, EventArgs e)
        {

        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            photoUploader pneumonia = new photoUploader();

            Panel panel = (Panel)this.Parent;
            panel.Controls.Clear();
            panel.Controls.Add(pneumonia);
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            if (ConnectionClass.ClientTCP == null)
            {
                //SetGif(false, true);
                MessageBox.Show("Sunteti momentan offline", "Tiroida");
                return;
            }


            Viewtests vt = new Viewtests();
            Panel pan1 = (Panel)this.Parent;
            pan1.Controls.Clear();
            pan1.Controls.Add(vt);
        }
    }
}
