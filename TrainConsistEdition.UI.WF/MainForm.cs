using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrainConsistEdition.BL.Controllers.Controllers;

namespace TrainConsistEdition.UI.WF
{
    public partial class MainForm : Form
    {
        private readonly CreateConsistController controller;

        public MainForm()
        {
            InitializeComponent();
            controller = new CreateConsistController();
            InitialListBox();
        }


        private void Button_AddLoco_Click(object sender, EventArgs e)
        {
            var validSelected = listBox_Loco.SelectedItem == null ? false : true;
            if (validSelected)
            {
                var module = listBox_Loco.SelectedItem.ToString().TrimEnd('.', 'x', 'm', 'l');
                var moduleCfg = listBox_Loco.SelectedItem.ToString();
                int locoCount = textBox_LocoCount.Text == "" ? 1 : int.Parse(textBox_LocoCount.Text);
                var payloadCoeff = 1.0;

                controller.AddTrainVehicle(module, moduleCfg, locoCount, payloadCoeff);
                dataGridView_Consists.Rows.Add(moduleCfg, locoCount);
                button_Serialize.Enabled = true;
                button_DeleteVehecle.Enabled = true;
                GetOkMessage("Локомотив готов!");
            }
            else
            {
                GetErrorMessage("Необходимо выбрать локомотив!");
            }

        }
        private void Button_AddVagon_Click(object sender, EventArgs e)
        {
            var validSelected = listBox_VagonName.SelectedItem == null ? false : true;
            if (validSelected)
            {
                var module = "passcar";
                var moduleCfg = listBox_VagonName.SelectedItem.ToString();
                int vagonCount = textBox_VagonCount.Text == "" ? 1 : int.Parse(textBox_VagonCount.Text);
                var payloadCoeff = textBox_Coeff.Text == "" || double.Parse(textBox_VagonCount.Text) > 1.0 ? 1.0 : double.Parse(textBox_LocoCount.Text);

                controller.AddTrainVehicle(module, moduleCfg, vagonCount, payloadCoeff);

                dataGridView_Consists.Rows.Add(moduleCfg, vagonCount);

                button_Serialize.Enabled = true;
                button_DeleteVehecle.Enabled = true;
                GetOkMessage("Вагон прицеплен!");
            }
            else
            {
                GetErrorMessage("Необходимо выбрать вагон!");
            }

        }
        private void Button_Serialize_Click(object sender, EventArgs e)
        {
            CreateConsist();

            var fileName = textBox_FileName.Text == "" ? "defailt" : textBox_FileName.Text;
            var serialaze = new SerializeController(controller, fileName);
            GetMessage(serialaze.SerializeConsist());
            void GetMessage(bool result)
            {
                if (result)
                {
                    GetOkMessage("Состав успешно создан!");
                }
                else
                {
                    GetErrorMessage("Не удалось сформировать состав!");
                }
            }
        }

        private void GetErrorMessage(string message)
        {
            MessageBox.Show(message, "Что-то пошло не так!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void GetOkMessage(string message)
        {
            MessageBox.Show(message, "Ура!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void InitialListBox()
        {
            var vehFolder = new DirectoryInfo(@"C:\Users\2334\AppData\Local\RRS\cfg\vehicles");
            var coulpFolder = new DirectoryInfo(@"C:\Users\2334\AppData\Local\RRS\cfg\couplings");
            
            foreach (var item in vehFolder.GetDirectories())
            {
                if (item.Name.Contains("IMR") || (item.Name.Contains("Fr")))
                {
                    listBox_VagonName.Items.Add(GetListBoxElement(item.ToString()));
                }
                else
                {
                    listBox_Loco.Items.Add(GetListBoxElement(item.ToString()));
                }

            }

            foreach (var item in coulpFolder.GetFiles())
            {
                listBox_CouplingType.Items.Add(GetListBoxElement(item.ToString()));
            }
   
        }

        private string GetListBoxElement(string item)
        {
            return item.TrimEnd('.', 'x', 'm', 'l').ToUpper();
        }

        private void CreateConsist()
        {
            var title = textBox_ConsistName.Text == "" ? "Поезд" : textBox_ConsistName.Text;
            var descr = textBox_Description.Text == "" ? title : textBox_Description.Text;
            var coupType = listBox_CouplingType.SelectedItem == null ? "ef-coupling" : listBox_CouplingType.SelectedItem.ToString();
            var cabine = textBox_CabineInVehicle.Text == "" ? 0 : int.Parse(textBox_CabineInVehicle.Text);
            var charginPress = textBox_ChargingPressure.Text == "" ? 0.5 : double.Parse(textBox_ChargingPressure.Text);
            var initPress = textBox_InitMainResPressure.Text == "" ? 0.9 : double.Parse(textBox_InitMainResPressure.Text);
            var noAir = checkBox_NoAir.Checked;

            controller.AddConsistOptions(title, descr, coupType, cabine, charginPress, initPress, noAir);
        }


        private void CheckBox_TrainOptions_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_TrainOptions.Checked)
            {
                groupBox_TrainOptons.Visible = false;
            }
            else
            {
                groupBox_TrainOptons.Visible = true;
            }
        }

        private void Button_DeleteVehecle_Click(object sender, EventArgs e)
        {
            
            if (!dataGridView_Consists.Rows[0].IsNewRow) 
            {
                
                var selectedVagon = dataGridView_Consists.CurrentCell.RowIndex;
                controller.RemoveTrainVehicle(selectedVagon);
            
                dataGridView_Consists.Rows.RemoveAt(dataGridView_Consists.CurrentCell.RowIndex);
                button_DeleteVehecle.Enabled = dataGridView_Consists.Rows[0].IsNewRow ? false : true;
                button_Serialize.Enabled = dataGridView_Consists.Rows[0].IsNewRow ? false : true;
            }
        }

    }
}
