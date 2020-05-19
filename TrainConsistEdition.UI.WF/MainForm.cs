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
using TrainConsistEdition.BL.Models.Trains;

namespace TrainConsistEdition.UI.WF
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Объявляем модель основного контроллера
        /// </summary>
        private CreateConsistController createController;
        private SerializeController serializeController;
        
        private string pathRRS;

        /// <summary>
        /// Инициализация формы
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            //Загружаем пути к необходимым папкам
            LoadSettings();
        }

        //Обработчики нажатия элементов меню
        #region Обработчики нажатия элементов меню

        /// <summary>
        /// ОБработчик выбора Создать новый состав
        /// </summary>
        private void MenuItem_CreateNewConsist_Click(object sender, EventArgs e)
        {
            Clear();
            createController = new CreateConsistController();
            panel_Main.Visible = true;
        }
        /// <summary>
        /// Обработчик выбора Открыть состав
        /// </summary>
        private void MenuItem_OpenConsist_Click(object sender, EventArgs e)
        {
            Clear();
            var openDialog = new OpenFileDialog
            {
                InitialDirectory = pathRRS
            };

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                var filename = openDialog.FileName;
                serializeController = new SerializeController(filename);


                var serializeResult = serializeController.OpenConsist();
                if (serializeResult.Item2)
                {
                    createController = new CreateConsistController(serializeResult.Item1);
                    SetOpenConsistOnDataGrid(serializeResult.Item1);
                    textBox_FileName.Text = GetListBoxElement(new DirectoryInfo(filename).Name).ToLower();
                    panel_Main.Visible = true;
                }
                else
                {
                    GetErrorMessage(serializeResult.Item3);
                    panel_Main.Visible = false;
                }




            }

        }
        /// <summary>
        /// Обработчик выбора Сохранить состав как
        /// </summary>
        private void MenuItem_SaveAs_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Обработчик выбора папки с игрой
        /// </summary>
        private void MenuItem_SetFolders_Click(object sender, EventArgs e)
        {
            //Вызываем метод получения пути к игре и записи его в файл настроек
            SaveSettings();
        }
        /// <summary>
        /// Обработчик выбора Выход
        /// </summary>
        private void MenuItem_Exit_Click(object sender, EventArgs e)
        {

        }

        #endregion

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
                var module = listBox_Loco.SelectedItem.ToString();
                var moduleCfg = listBox_Loco.SelectedItem.ToString();
                int locoCount = (int.TryParse(textBox_LocoCount.Text, out int count) && count != 0) ? count : 1;
                var payloadCoeff = 1.0;

                //Передаем параметры в контроллер дл инициализации модели
                createController.AddTrainVehicle(module, moduleCfg, locoCount, payloadCoeff);
                //Вызываем вспомогательный метод управления dataGridView_Consists и кнопками
                SetDataGrid(moduleCfg, locoCount, payloadCoeff);
                listBox_Loco.ClearSelected();

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
                createController.AddTrainVehicle(module, moduleCfg, vagonCount, payloadCoeff);
                //Добавлям выбранный локомотив в dataGridView_Consists
                SetDataGrid(moduleCfg, vagonCount, payloadCoeff);
                listBox_VagonName.ClearSelected();
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
            CreateConsistOption();

            //Получаем от пользователя имя итогового файла, по умолчанию имя файла "default"
            var fileName = textBox_FileName.Text == "" ? "defailt" : textBox_FileName.Text;
            //Создаем контроллер сериализации и передаем ему основной контроллер и имя итогового файла
            var serialaze = new SerializeController(createController, fileName);
            //Сериализируем итоговый поезд в XML файл и передаем результат выполнения в MessageBox
            var serializeResult = serialaze.SerializeConsist(pathRRS);
            if (serializeResult.Item1)
            {
                GetOkMessage(serializeResult.Item2);
            }
            else
            {
                GetErrorMessage(serializeResult.Item2);
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
                GetMessage(createController.RemoveTrainVehicle(selectedVagon));

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

        private void Button_Change_Click(object sender, EventArgs e)
        {
            string module = "";
            string moduleCfg = "";
            
            if(listBox_Loco.SelectedItems.Count != 0)
            {
                module = listBox_Loco.SelectedItem.ToString();
                moduleCfg = listBox_Loco.SelectedItem.ToString();

            }
            if(listBox_VagonName.SelectedItems.Count != 0)
            {
                module = "passcar";
                moduleCfg = listBox_VagonName.SelectedItem.ToString();
            }

            DataGridView_Consists_ChangeVehicle(module, moduleCfg);
        }

        private void Button_Clear_Click(object sender, EventArgs e)
        {
            Clear();
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
            //Объявляем контроллер управлениян настройками
            var settingsController = new ApplicationSettingsController();
            //Пытаемся получить настройки из конфигурационного файла
            var res = settingsController.GetApplicationDirectory();
            if (res)
            {
                //Если настройки получены проводим инициализацию UI
                //Получае пути к необходимым папкам игры в кортеж
                (string, string, string) dirs = settingsController.GetVehecleAndCoupleTypeDirrectores();
                //Получаем путь к RRS
                pathRRS = dirs.Item1;
                //Вызываем метод применяющий начальные настройки отображения данных в UI
                InitialListBox(dirs.Item2, dirs.Item3);
            }
            else //Иначе просим пользователя указать каталог с игрой
            {
                GetOkMessage("Для начала работы приложения в меню укажите каталог с установленным RRS");
            }

        }

        /// <summary>
        /// Метод позволяющий пьзователю выбрать каталог с игрой и записать его в конфигурационный файл
        /// </summary>
        private void SaveSettings()
        {
            //Объявляем контроллер управлениян настройками
            var settingsController = new ApplicationSettingsController();
            
            //Открываем диалог выбора каталога
            var folder = new FolderBrowserDialog();
            //Проверяем выбрал ли пользователь директорию
            if (folder.ShowDialog() == DialogResult.OK)
            {
                //Получаем путь к выбранной директории
                var path = folder.SelectedPath;
                //Проверяем, что пользователь выбрал верную директорию с RRS
                if (Directory.Exists(path + @"/cfg"))
                {
                    //Вызываем на контроллере метод устанавливающий директорию игры и передаем внего путь
                    settingsController.SetApplicationDirectory(path);
                    //Вызываем локальную функцию получения настроек
                    LoadSettings();
                }
                else
                {
                    GetErrorMessage("Убедитесь, что RRS установлен или правильно укажите каталог с игрой");
                }
            }
            else
            {
                GetErrorMessage("Убедитесь, что RRS установлен или правильно укажите каталог с игрой");
            }
        }

        /// <summary>
        /// Метод передает в модель свойст и характеристик поезда данные введенные пользователем
        /// </summary>
        private void CreateConsistOption()
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
            createController.AddConsistOptions(title, descr, coupType, cabine, charginPress, initPress, noAir);
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

        private void Clear()
        {
            createController = null;
            textBox_FileName.Clear();
            textBox_ConsistName.Clear();
            textBox_Description.Clear();
            dataGridView_Consists.Rows.Clear();
            button_DeleteVehecle.Enabled = false;
            button_Serialize.Enabled = false;
            button_Clear.Enabled = false;
            button_Change.Enabled = false;
        }


        #endregion

        //ListBox
        #region ListBox

        /// <summary>
        /// Начальная инициализация UI
        /// </summary>
        private void InitialListBox(string dirVeh, string dirCouple)
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

        private void ListBox_Loco_MouseClick(object sender, MouseEventArgs e)
        {
            Button_AddLoco_Click(sender, e);
        }

        private void ListBox_VagonName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Button_AddVagon_Click(sender, e);
        }

        #endregion

        //DataGrid
        #region DataGrid


        /// <summary>
        /// Вспомогательный метод управления dataGridView_Consists и кнопками
        /// </summary>
        /// <param name="moduleCfg">Наименование</param>
        /// <param name="count">Количество</param>
        private void SetDataGrid(string moduleCfg, int count, double coeff)
        {
            //Отображаем состав поезда
            groupBox_Consist.Enabled = true;
            //Добавлям выбранный локомотив в dataGridView_Consists
            dataGridView_Consists.Rows.Add(moduleCfg, count, coeff);
            //Разблокируем кнопки
            button_Serialize.Enabled = true;
            button_DeleteVehecle.Enabled = true;
            button_Change.Enabled = true;
            button_Clear.Enabled = true;

        }

        /// <summary>
        /// Обаботчик редактирования значения ячейки "количество" в DataGridView_Consists
        /// </summary>
        private void DataGridView_Consists_CellAndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //Проверяем есть ли данные в dataGridView_Consists
            if (!dataGridView_Consists.Rows[0].IsNewRow)
            {
                //Получаем индекс выбранной пользователем единицы подвижного состава
                var selectedVagon = dataGridView_Consists.CurrentCell.RowIndex;
                
                var column = dataGridView_Consists.CurrentCell.ColumnIndex;
                (bool, string) editResult;
                double editValue;
                if (column == 1) //Колонка с количеством вагонов
                {
                    //Получаем количество выбранной пользователем единицы подвижного состава
                    editValue = int.TryParse(dataGridView_Consists.CurrentCell.Value.ToString(), out int valueCount) && valueCount != 0 ? (int)valueCount : (int)1;
                    //Вызываем перегрузку метода редактирования количества единицы подвижного состава
                    editResult = createController.EditTrainVehicle(selectedVagon, (int)editValue);
                }
                else //Колонка с коэф.загрузки
                {
                    //Получаем количество выбранной пользователем единицы подвижного состава
                    editValue = double.TryParse(dataGridView_Consists.CurrentCell.Value.ToString(), out double valueCoef) && valueCoef > 0 && valueCoef <= 1.0 ? valueCoef : 1.0;
                    //Вызываем перегрузку метод редактирования коеэф. загруженности подвижного состава
                    editResult = createController.EditTrainVehicle(selectedVagon, editValue);
                }

                //По результату, меняем значение ячейки в dataGridView_Consists и вызывающая MessageBox в соответствии с итогами проведения сериализации
                if (editResult.Item1)
                {
                    //Меняем значение ячейки количетсво в dataGridView_Consists
                    dataGridView_Consists.CurrentCell.Value = editValue;
                    GetOkMessage(editResult.Item2);
                }
                else
                {
                    GetErrorMessage(editResult.Item2);
                }
                
            }
        }

        private void DataGridView_Consists_ChangeVehicle(string module, string moduleCfg)
        {
            //Проверяем есть ли данные в dataGridView_Consists
            if (!dataGridView_Consists.Rows[0].IsNewRow)
            {
                //Получаем индекс выбранной пользователем единицы подвижного состава
                var selectedVagon = dataGridView_Consists.CurrentCell.RowIndex;

                
                (bool, string) editResult;
                editResult = createController.EditTrainVehicle(selectedVagon, module, moduleCfg);
                

                //По результату, меняем значение ячейки в dataGridView_Consists и вызывающая MessageBox в соответствии с итогами проведения сериализации
                if (editResult.Item1)
                {
                    //Меняем значение ячейки количетсво в dataGridView_Consists
                    dataGridView_Consists.CurrentCell.Value = moduleCfg;
                    GetOkMessage(editResult.Item2);
                }
                else
                {
                    GetErrorMessage(editResult.Item2);
                }

            }
        }

        /// <summary>
        /// Метод устанавливает значения из открытого файла состава в DataGrid и поля формы
        /// </summary>
        /// <param name="model">Модель состава</param>
        private void SetOpenConsistOnDataGrid(ConsistModel model)
        {
            
            //Устанавливаем значения в DataGrid
            foreach (var item in model.Vehicle)
            {
                SetDataGrid(item.ModuleConfig, item.Count, item.PayloadCoeff);
            }
            //Устанавливаем значения в поля формы
            textBox_Description.Text = model.Common.Description;
            textBox_ConsistName.Text = model.Common.Title;
            textBox_CabineInVehicle.Text = model.Common.CabineInVehicle.ToString();
            textBox_ChargingPressure.Text = model.Common.ChargingPressure.ToString();
            textBox_InitMainResPressure.Text = model.Common.InitMainResPressure.ToString();
            checkBox_NoAir.Checked = model.Common.NoAir;
            listBox_CouplingType.SelectedItem = model.Common.CouplingModule;

            
        }

        #endregion

    }
}
