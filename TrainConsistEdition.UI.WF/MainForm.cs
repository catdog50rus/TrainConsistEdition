using System;
using System.IO;
using System.Windows.Forms;
using TrainConsistEdition.BL.Controllers.Controllers;
using TrainConsistEdition.BL.Models.Trains;

namespace TrainConsistEdition.UI.WF
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Объявляем основной контроллер
        /// </summary>
        private CreateConsistController createController;
        /// <summary>
        /// Объявляем контроллер сериализации
        /// </summary>
        private SerializeController serializeController;
        /// <summary>
        /// Объявляем путь к игре RRS
        /// </summary>
        private string pathTrains;

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
            //Очищаем форму
            Clear();
            //Создаем контроллер
            createController = new CreateConsistController();
            //Включаем отображение элеметов формы
            panel_Main.Visible = true;
        }
        /// <summary>
        /// Обработчик выбора Открыть состав
        /// </summary>
        private void MenuItem_OpenConsist_Click(object sender, EventArgs e)
        {
            //Очищаем форму
            Clear();
            //Открываем стандартный диалог Win, начальная директория с составами в RRS, фильтр файловпо расширению xml
            var openDialog = new OpenFileDialog
            {
                InitialDirectory = pathTrains,
                Filter = "xml файлы (*.xml)|*.xml"
            };

            if (openDialog.ShowDialog() == DialogResult.OK)//Если пользователь выбрал файл
            {
                var filename = openDialog.FileName; //Получаем полный путь к файлу

                //Создаем контроллер сериализации и передаем в него путь к файлу
                serializeController = new SerializeController(filename);

                //Вызываем метод десериализации, результат получаем в кортеж(Модель, Результат действия (да\нет), Сообщение)
                var serializeResult = serializeController.OpenConsist();
                
                if (serializeResult.Item2)//Если удачно
                {
                    createController = new CreateConsistController(serializeResult.Item1); //Создаем контроллер и передаем в его модель
                    SetOpenConsistOnDataGrid(serializeResult.Item1); //Передаем в DataGrid модель
                    panel_Main.Visible = true; //Отображаем элементы формы
                }
                else //При неудаче
                {
                    GetErrorMessage(serializeResult.Item3);//Выводим сообщение об ошибке
                    panel_Main.Visible = false;//Элементы формы не отображаются
                }
            }

        }
        /// <summary>
        /// Обработчик выбора папки с игрой
        /// </summary>
        private void MenuItem_SetFolders_Click(object sender, EventArgs e)
        {
            //Вызываем метод получения пути к игре и записи его в файл настроек
            SaveSettings();
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
                //Получаем параметры локомотива
                var loco = GetLoco();

                //Передаем параметры в контроллер дл инициализации модели
                createController.AddTrainVehicle(loco.Item1, loco.Item2, loco.Item3, loco.Item4);
                //Вызываем вспомогательный метод управления dataGridView_Consists и кнопками
                SetDataGrid(loco.Item2, loco.Item3, loco.Item4);
                

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
                //Получаем данные из элементов формы
                var vagon = GetVagon();
                //Передаем параметры в контроллер дл инициализации модели
                createController.AddTrainVehicle(vagon.Item1, vagon.Item2, vagon.Item3, vagon.Item4);
                //Добавлям выбранный локомотив в dataGridView_Consists
                SetDataGrid(vagon.Item2, vagon.Item3, vagon.Item4);
                
            }
            else
            {
                //Вызываем MessageBox
                GetErrorMessage("Необходимо выбрать вагон!");
            }

        }

        /// <summary>
        /// Обработчик нажатия на кнопку "Сохранить состав"
        /// </summary>
        private void Button_Serialize_Click(object sender, EventArgs e)
        {
            //Вызываем метод сохранения характеристик и описания поезда
            CreateConsistOption();

            //Открываем диалогоое окно записи файла.
            //Начальнаядиректория с составами в RRS
            //Фильтр файлов .xml
            var saveDialog = new SaveFileDialog()
            {
                InitialDirectory = pathTrains,
                Filter = "xml файлы (*.xml)|*.xml"
            };
            if (saveDialog.ShowDialog() == DialogResult.OK) //Если пользователь выбрал файл сохранения
            {
                //Получаем полный путь к итоговому файлу
                var file = saveDialog.FileName;
                //Создаем контроллер сериализации и передаем ему основной контроллер и итоговый файл
                var serialaze = new SerializeController(createController, file);
                //Сериализируем итоговый поезд в XML файл, получаем кореж (результат выполнения и текст сообщения), все передаем в MessageBox
                var serializeResult = serialaze.SerializeConsist();
                if (serializeResult.Item1)
                {
                    GetOkMessage(serializeResult.Item2);
                }
                else
                {
                    GetErrorMessage(serializeResult.Item2);
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
                //Вызываем метод удаления единицы подвижного состава из списка модели и получаем кортеж(Результат удаления, текст сообщения)
                var removeResult = createController.RemoveTrainVehicle(selectedVagon);
                if (removeResult.Item1) //Если успещшно
                {
                    //Удаляем выбранную единицу подвижного состава из dataGridView_Consists
                    dataGridView_Consists.Rows.RemoveAt(dataGridView_Consists.CurrentCell.RowIndex);
                    //В случае, если в dataGridView_Consists нет больше данных блокируем кнопки "Отцепить" и "Готово!"
                    button_DeleteVehecle.Enabled = dataGridView_Consists.Rows[0].IsNewRow ? false : true;
                    button_Serialize.Enabled = dataGridView_Consists.Rows[0].IsNewRow ? false : true;
                    groupBox_Consist.Enabled = dataGridView_Consists.Rows[0].IsNewRow ? false : true;
                    //Отображаем сообщение
                    GetOkMessage(removeResult.Item2);
                }
                else
                {
                    GetErrorMessage(removeResult.Item2);
                }
            }
        }

        /// <summary>
        /// Метод обработки нажатия кнопки "Заменить"
        /// Метод позволяет заменить выбранный в DataGrid вагон или локомотив целиком
        /// </summary>
        private void Button_Change_Click(object sender, EventArgs e)
        {
            //Объявляем лоальные переменные
            string module = "";
            string moduleCfg = "";
            
            if(listBox_Loco.SelectedItems.Count != 0)//Присваеваем пременным значения, если выбран локомотив
            {
                var loco = GetLoco();
                module = loco.Item1;
                moduleCfg = loco.Item2;
            }
            if(listBox_VagonName.SelectedItems.Count != 0)//Присваеваем пременным значения, если выбран вагон
            {
                var vagon = GetVagon();
                module = vagon.Item1;
                moduleCfg = vagon.Item2;
            }
            
            //Передаем в DataGrid новые значения
            DataGridView_Consists_ChangeVehicle(module, moduleCfg);
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Очистить"
        /// </summary>
        private void Button_Clear_Click(object sender, EventArgs e)
        {
            //Вызываем метод очистки элементов формы
            Clear();
        }

        /// <summary>
        /// Обработчик нажатия "Крестика" на форме
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Вызываем окно подтверждения выхода
            if (MessageBox.Show("Вся не сохраненная работа будет утеряна!\r\n Вы желаете выйти из приложения?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.No)
                e.Cancel = true;
            else
                e.Cancel = false;
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
            //var settingsController = new ApplicationSettingsController();
            //Пытаемся получить настройки из конфигурационного файла
            var result = SettingsController.GetPathRRSTrains();// settingsController.GetApplicationDirectory();
            if (result.Item1)
            {
                //Если настройки получены проводим инициализацию UI
                //Получаем путь к Составам
                pathTrains = result.Item2;
                //Вызываем метод применяющий начальные настройки отображения данных в UI
                InitialListBox();
                MenuItem_CreateNewConsist.Enabled = true;
                MenuItem_OpenConsist.Enabled = true;
                MenuItem_SetFolders.Enabled = false;
            }
            else //Иначе просим пользователя указать каталог с игрой
            {
                GetOkMessage(result.Item2);
                MenuItem_CreateNewConsist.Enabled = false;
                MenuItem_OpenConsist.Enabled = false;
            }

        }

        /// <summary>
        /// Метод позволяющий пьзователю выбрать каталог с игрой и записать его в конфигурационный файл
        /// </summary>
        private void SaveSettings()
        {
            //Объявляем контроллер управлениян настройками
            //var settingsController = new ApplicationSettingsController();
            
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
                    SettingsController.SetPathRRS(path);
                    //Вызываем метод получения настроек
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
        /// Метод для получения Данных локомотива из элементов формы
        /// </summary>
        /// <returns>Возвращает кортеж </returns>
        private (string, string, int, double) GetLoco()
        {
            //Получаем значения параметров
            //Для параметра Module необходимо обрезать номер локомотива. если есть
            //Находим позицию начала номера
            int pos = listBox_Loco.SelectedItem.ToString().LastIndexOf('-');
            //Если номер отсутствует позиция равна длине строки имеется обрезаем его
            if (pos == -1) pos = listBox_Loco.SelectedItem.ToString().Length;
            //Получаем значение с учетом обрезки номера
            string module = listBox_Loco.SelectedItem.ToString().Substring(0, pos);
            string moduleCfg = listBox_Loco.SelectedItem.ToString();
            int locoCount = (int.TryParse(textBox_LocoCount.Text, out int count) && count != 0) ? count : 1;
            double payloadCoeff = 1.0;
            //Убираем маркер выделения
            listBox_Loco.ClearSelected();
            return (module, moduleCfg, locoCount, payloadCoeff);

        }

        /// <summary>
        /// Метод для получения данных вагона из элементов формы
        /// </summary>
        /// <returns>Возвращает кортеж </returns>
        private (string, string, int, double) GetVagon()
        {
            //Получаем значения параметров
            var module = "passcar";
            var moduleCfg = listBox_VagonName.SelectedItem.ToString();
            int vagonCount = (int.TryParse(textBox_VagonCount.Text, out int count) && count != 0) ? count : 1;
            var payloadCoeff = (double.TryParse(textBox_Coeff.Text, out double coeff) && coeff <= 1.0 && coeff >= 0) ? coeff : 1.0;
            //Убираем маркер выделения
            listBox_VagonName.ClearSelected();
            return (module, moduleCfg, vagonCount, payloadCoeff);
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
            var noAir = checkBox_NoAir.Checked ? 1 : 0;

            //Передаем в контроллер данные пользователя
            createController.AddConsistOptions(title, descr, coupType, cabine, charginPress, initPress, noAir);
        }

        /// <summary>
        /// Метод обрабатывающий CheckBox_TrainOptions
        /// Позволяет скрывать или показывать характеристики состава
        /// В общем случае, характеристики состава скрыты, все значения устанавливаются по умолчанию, 
        /// рекомендованными разработчиками игры
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

        /// <summary>
        /// Метод очищает форму и зануляет контроллер
        /// Блокирует кнопки управления
        /// </summary>
        private void Clear()
        {
            createController = null;
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
        private void InitialListBox()
        {
            var listData = SettingsController.GetListData();
            foreach (var item in listData.Item1)
            {
                listBox_CouplingType.Items.Add(item);
            }
            foreach (var item in listData.Item2)
            {
                listBox_Loco.Items.Add(item);
            }
            foreach (var item in listData.Item3)
            {
                listBox_VagonName.Items.Add(item);
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

        /// <summary>
        /// Обаботчик замены локомотива или вагона в DataGridView_Consists
        /// </summary>
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
            if(model.Common.NoAir == 0 || model.Common.NoAir == 1)
            {
                checkBox_NoAir.Checked = model.Common.NoAir == 0 ? false : true;
            }
            else
            {
                checkBox_NoAir.Checked = false;
            }
            
            listBox_CouplingType.SelectedItem = model.Common.CouplingModule;

            
        }

        #endregion
    }
}
