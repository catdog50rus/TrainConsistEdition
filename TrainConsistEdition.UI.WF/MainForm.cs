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
        /// <summary>
        /// Объявляем модель основного контроллера
        /// </summary>
        private readonly CreateConsistController controller;

        /// <summary>
        /// Инициализация формы
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            //Создаем основной контроллер
            controller = new CreateConsistController();
            //Начальные настройки отображения данных в UI
            InitialListBox();
        }

        //Обработчики событий нажатия на кнопки
        #region Обработчики событий нажатия на кнопки

        /// <summary>
        /// Обработчик нажатия на кнопку "Добавить локомотив"
        /// </summary>
        private void Button_AddLoco_Click(object sender, EventArgs e)
        {
            //Проверяем выбрал ли пользователь локомотив из listBox_Loco
            var validSelected = listBox_Loco.SelectedItem == null ? false : true;
            if (validSelected)
            {
                //Получаем значения параметров
                var module = listBox_Loco.SelectedItem.ToString().TrimEnd('.', 'x', 'm', 'l');
                var moduleCfg = listBox_Loco.SelectedItem.ToString();
                int locoCount = (int.TryParse(textBox_LocoCount.Text, out int count) && count !=0) ? count: 1;
                var payloadCoeff = 1.0;

                //Передаем параметры в контроллер дл инициализации модели
                controller.AddTrainVehicle(module, moduleCfg, locoCount, payloadCoeff);
                //Добавлям выбранный локомотив в dataGridView_Consists
                dataGridView_Consists.Rows.Add(moduleCfg, locoCount);
                //Разблокируем кнопки
                button_Serialize.Enabled = true;
                button_DeleteVehecle.Enabled = true;
                //Вызываем MessageBox
                GetOkMessage("Локомотив готов!");
            }
            else
            {
                //Вызываем MessageBox
                GetErrorMessage("Необходимо выбрать локомотив!");
            }

        }

        /// <summary>
        /// Обработчик нажатия на кнопку "Прицепить вагон"
        /// </summary>
        private void Button_AddVagon_Click(object sender, EventArgs e)
        {
            //Проверяем выбрал ли пользователь тип вагона из listBox_VagonName
            var validSelected = listBox_VagonName.SelectedItem == null ? false : true;
            if (validSelected)
            {
                //Получаем значения параметров
                var module = "passcar";
                var moduleCfg = listBox_VagonName.SelectedItem.ToString();
                int vagonCount = (int.TryParse(textBox_VagonCount.Text, out int count) && count != 0)? count : 1;
                var payloadCoeff = (double.TryParse(textBox_Coeff.Text, out double coeff) && coeff <= 1.0 && coeff >= 0) ? coeff : 1.0;

                //Передаем параметры в контроллер дл инициализации модели
                controller.AddTrainVehicle(module, moduleCfg, vagonCount, payloadCoeff);
                //Добавлям выбранный локомотив в dataGridView_Consists
                dataGridView_Consists.Rows.Add(moduleCfg, vagonCount);
                //Разблокируем кнопки
                button_Serialize.Enabled = true;
                button_DeleteVehecle.Enabled = true;
                //Вызываем MessageBox
                GetOkMessage("Вагон прицеплен!");
            }
            else
            {
                //Вызываем MessageBox
                GetErrorMessage("Необходимо выбрать вагон!");
            }

        }

        /// <summary>
        /// Обработчик нажатия на кнопку "Готово"
        /// </summary>
        private void Button_Serialize_Click(object sender, EventArgs e)
        {
            //Вызываем метод, создающий свойства и характеристики поезда
            CreateConsist();

            //Получаем от пользователя имя итогового файла, по умолчанию имя файла "default"
            var fileName = textBox_FileName.Text == "" ? "defailt" : textBox_FileName.Text;
            //Создаем контроллер сериализации и передаем ему основной контроллер и имя итогового файла
            var serialaze = new SerializeController(controller, fileName);
            //Сериализируем итоговый поезд в XML файл и передаем результат выполнения в MessageBox
            GetMessage(serialaze.SerializeConsist());
            //Локальная функция, вызывающая MessageBox в соответствии с итогами проведения сериализации
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

        /// <summary>
        /// Метод удаляет единицу подвижного состава из списка подвижного состава поезда
        /// </summary>
        private void Button_DeleteVehecle_Click(object sender, EventArgs e)
        {

            //Проверяем есть ли данные в dataGridView_Consists
            if (!dataGridView_Consists.Rows[0].IsNewRow)
            {

                //Получаем индекс выбранной пользователем единицы подвижного состава
                var selectedVagon = dataGridView_Consists.CurrentCell.RowIndex;
                //Вызываем метод удаления единицы подвижного состава из списка модели
                controller.RemoveTrainVehicle(selectedVagon);

                //Удаляем выбранную единицу подвижного состава из dataGridView_Consists
                dataGridView_Consists.Rows.RemoveAt(dataGridView_Consists.CurrentCell.RowIndex);
                //В случае, если в dataGridView_Consists нет больше данных блокируем кнопки "Отцепить" и "Готово!"
                button_DeleteVehecle.Enabled = dataGridView_Consists.Rows[0].IsNewRow ? false : true;
                button_Serialize.Enabled = dataGridView_Consists.Rows[0].IsNewRow ? false : true;
            }
        }

        /// <summary>
        /// Метод передает в модель свойст и характеристик поезда данные введенные пользователем
        /// </summary>
        private void CreateConsist()
        {
            //Если поле не заполнено, значание по умолчанию "Поезд"
            var title = textBox_ConsistName.Text == "" ? "Поезд" : textBox_ConsistName.Text;
            //Если поле е заполнено, значение по умолчанию равно полю title
            var descr = textBox_Description.Text == "" ? title : textBox_Description.Text;

            //Поля характеристик поезда.
            //В общем случае заполнены значениями по умолчанию рекомендованными разработчиками игры
            var coupType = listBox_CouplingType.SelectedItem == null ? "ef-coupling" : listBox_CouplingType.SelectedItem.ToString();
            var cabine = textBox_CabineInVehicle.Text == "" ? 0 : int.Parse(textBox_CabineInVehicle.Text);
            var charginPress = textBox_ChargingPressure.Text == "" ? 0.5 : double.Parse(textBox_ChargingPressure.Text);
            var initPress = textBox_InitMainResPressure.Text == "" ? 0.9 : double.Parse(textBox_InitMainResPressure.Text);
            var noAir = checkBox_NoAir.Checked;

            //Передаем в контроллер данные пользователя
            controller.AddConsistOptions(title, descr, coupType, cabine, charginPress, initPress, noAir);
        }

        #endregion

        // Вспомогательные методы
        #region Вспомогательные методы

        /// <summary>
        /// Метод вызова MessageBox сообщающий об ошибке
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        private void GetErrorMessage(string message)
        {
            MessageBox.Show(message, "Что-то пошло не так!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Метод вызова MessageBox сообщающий о выполнении действия
        /// </summary>
        /// <param name="message">Отображаемый текст</param>
        private void GetOkMessage(string message)
        {
            MessageBox.Show(message, "Ура!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Начальная инициализация UI
        /// </summary>
        private void InitialListBox()
        {
            //Путь к папке с единицами подвижного состава
            var vehFolder = new DirectoryInfo(@"C:\Users\2334\AppData\Local\RRS\cfg\vehicles");
            //Путь к папке с модулями сцепки
            var coulpFolder = new DirectoryInfo(@"C:\Users\2334\AppData\Local\RRS\cfg\couplings");
            //Заполнение ListBox_Loco и ListBox_VagonName данными на основании файлов игры
            foreach (var item in vehFolder.GetDirectories())
            {
                //Отбираем какой подвижной состав относится к вагонам, а какой к локомотивам
                //Заполняем ListBox_VagonName
                if (item.Name.Contains("IMR") || (item.Name.Contains("Fr")))
                {
                    listBox_VagonName.Items.Add(GetListBoxElement(item.ToString()));
                }
                //Заполняем ListBox_Loco
                else
                {
                    listBox_Loco.Items.Add(GetListBoxElement(item.ToString()));
                }

            }
            //Заполняем ListBox_CouplingType на основании файлов игры
            foreach (var item in coulpFolder.GetFiles())
            {
                listBox_CouplingType.Items.Add(GetListBoxElement(item.ToString()));
            }
   
        }

        /// <summary>
        /// Метод убирающий расширение xml из имени сонфигурационного файла
        /// </summary>
        /// <param name="item">элемент коллекции имен файлов</param>
        private string GetListBoxElement(string item)
        {
            return item.TrimEnd('.', 'x', 'm', 'l').ToUpper();
        }

        /// <summary>
        /// Метод обрабатывающий CheckBox_TrainOptions
        /// Позволяет скрывать или показывать характеристики состава
        /// В общем случае, характеристики состава скрыты, все значения устанавливаются по умолчанию, рекомендованными разработчиками игры
        /// </summary>
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
        #endregion

        


        

        

    }
}
