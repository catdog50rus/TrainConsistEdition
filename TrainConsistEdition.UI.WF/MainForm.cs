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
            //Загружаем пути к необходимым папкам
            LoadSettings();

            //Создаем основной контроллер
            controller = new CreateConsistController();

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
                int locoCount = (int.TryParse(textBox_LocoCount.Text, out int count) && count != 0) ? count : 1;
                var payloadCoeff = 1.0;

                //Передаем параметры в контроллер дл инициализации модели
                controller.AddTrainVehicle(module, moduleCfg, locoCount, payloadCoeff);
                //Вызываем вспомогательный метод управления dataGridView_Consists и кнопками
                SetDataGrid(moduleCfg, locoCount);
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
        /// Вспомогательный метод управления dataGridView_Consists и кнопками
        /// </summary>
        /// <param name="moduleCfg">Наименование</param>
        /// <param name="count">Количество</param>
        private void SetDataGrid(string moduleCfg, int count)
        {
            //Отображаем состав поезда
            groupBox_Consist.Enabled = true;
            //Добавлям выбранный локомотив в dataGridView_Consists
            dataGridView_Consists.Rows.Add(moduleCfg, count);
            //Разблокируем кнопки
            button_Serialize.Enabled = true;
            button_DeleteVehecle.Enabled = true;
            
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
                SetDataGrid(moduleCfg, vagonCount);
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
                GetMessage(controller.RemoveTrainVehicle(selectedVagon));

                //Удаляем выбранную единицу подвижного состава из dataGridView_Consists
                dataGridView_Consists.Rows.RemoveAt(dataGridView_Consists.CurrentCell.RowIndex);
                //В случае, если в dataGridView_Consists нет больше данных блокируем кнопки "Отцепить" и "Готово!"
                button_DeleteVehecle.Enabled = dataGridView_Consists.Rows[0].IsNewRow ? false : true;
                button_Serialize.Enabled = dataGridView_Consists.Rows[0].IsNewRow ? false : true;
                groupBox_Consist.Enabled = dataGridView_Consists.Rows[0].IsNewRow ? false : true;
                //Локальная функция, вызывающая MessageBox в соответствии с итогами проведения вызова
                void GetMessage(bool result)
                {
                    if (result)
                    {
                        GetOkMessage("Состав успешно отредактирован!");
                    }
                    else
                    {
                        GetErrorMessage("Не удалось отредоктировать состав!");
                    }
                }
            }
        }

        /// <summary>
        /// Обаботчик редактирования значения ячейки "количество" в DataGridView_Consists
        /// </summary>
        private void DataGridView_Consists_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //Проверяем есть ли данные в dataGridView_Consists
            if (!dataGridView_Consists.Rows[0].IsNewRow)
            {
                //Получаем индекс выбранной пользователем единицы подвижного состава
                var selectedVagon = dataGridView_Consists.CurrentCell.RowIndex;
                //Получаем количество выбранной пользователем единицы подвижного состава
                var vagonCount = int.TryParse(dataGridView_Consists.CurrentCell.Value.ToString(), out int value) ? value : 1;

                //Вызываем метод редактирования количества единицы подвижного состава
                //и передаем результат в локадьную функию вызова MessageBox 
                GetMessage(controller.EditTrainVehicleCount(selectedVagon, vagonCount));
                //Меняем значение ячейки количетсво в dataGridView_Consists
                dataGridView_Consists.CurrentCell.Value = int.TryParse(value.ToString(), out int res) ? res : 1;

                //Локальная функция, вызывающая MessageBox в соответствии с итогами проведения сериализации
                void GetMessage(bool result)
                {
                    if (result)
                    {
                        GetOkMessage("Состав успешно отредактирован!");
                    }
                    else
                    {
                        GetErrorMessage("Не удалось отредоктировать состав!");
                    }
                }
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
        /// Метод вызывающий начальные настройки директорий
        /// </summary>
        private void LoadSettings()
        {
            //Объявляем контроллер управлениян астройками
            var settingsController = new ApplicationSettingsController();
            //Пытаемся получить настройки из конфигурационного файла
            var res = settingsController.GetApplicationDirectory();
            if (res)
            {
                //Если настройки получены проводим инициализацию UI
                GetSettings();
            }
            else //Иначе просим пользователя указать каталог с игрой
            {
                GetOkMessage("Для начала работы приложения укажите каталог с установленный RRS");
                //Открываем диалог выбора каталога
                var folder = new FolderBrowserDialog();
                //Проверяем выбрал ли пользователь директорию
                if (folder.ShowDialog() == DialogResult.OK)
                {
                    //Получаем путь к выбранной директории
                    var path = folder.SelectedPath;
                    //Проверяем, что пользователь выбрал веную директорию с RRS
                    if (Directory.Exists(path + @"/cfg"))
                    {
                        //Вызываем на контроллере метод устанавливающий директорию игры и передаем внего путь
                        settingsController.SetApplicationDirectory(path);
                        //Вызываем локальную функцию получения настроек
                        GetSettings();
                    }
                }
                else
                {
                    GetErrorMessage("Убедитесь, что RRS установлена или правильно укажите каталог с игрой");
                    //Перезапускаем метод установки начального каталога с игрой
                    LoadSettings();
                }

            }

            //Локаьная функция получающая пути к папкам и 
            void GetSettings()
            {
                //Получае пути к необходимым папкам игры в кортеж
                (string, string) dirs = settingsController.GetVehecleAndCoupleTypeDirrectores();
                //Вызываем метод применяющий начальные настройки отображения данных в UI
                InitialListBox(dirs.Item1, dirs.Item2);
            }
        }

        /// <summary>
        /// Начальная инициализация UI
        /// </summary>
        private void InitialListBox(string dirVeh,string dirCouple)
        {
            //Путь к папке с единицами подвижного состава
            var vehFolder = new DirectoryInfo(@dirVeh);
            //Путь к папке с модулями сцепки
            var coulpFolder = new DirectoryInfo(@dirCouple);
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
