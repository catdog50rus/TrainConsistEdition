using RRSTrainConsistEdition.Core.Models;
using RRSTrainConsistEdition.Infrastructure.DI;
using RRSTrainConsistEdition.Infrastructure.DI.Settings;
using System;
using System.IO;
using System.Windows.Forms;

namespace TrainConsistEdition.UI.WF
{
    public partial class MainForm : Form
    {
        //Поля
        private readonly Configuration _configuration;

        private ICreateConsistService _consistService;
        private readonly ISettingService _settingService;
        private IConsist _consist;

        private string pathTrains;

        public MainForm()
        {
            InitializeComponent();
            _configuration = new Configuration();

            _settingService = _configuration.Container.GetInstance<ISettingService>();
            LoadSettings();
            _consistService = _configuration.Container.GetInstance<ICreateConsistService>();
        }

        #region Обработчики нажатия элементов меню

        /// <summary>
        /// ОБработчик выбора Создать новый состав
        /// </summary>
        private void MenuItem_CreateNewConsist_Click(object sender, EventArgs e)
        {
            Clear();
            panel_Main.Visible = true;
            _consist = _configuration.Container.GetInstance<IConsist>();
            _consistService = _configuration.Container.GetInstance<ICreateConsistService>();
        }

        /// <summary>
        /// Обработчик выбора Открыть состав
        /// </summary>
        private void MenuItem_OpenConsist_Click(object sender, EventArgs e)
        {
            Clear();
            var openDialog = new OpenFileDialog
            {
                InitialDirectory = pathTrains,
                Filter = "xml файлы (*.xml)|*.xml"
            };

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                var filename = openDialog.FileName;
                
                try
                {
                    _consist = _configuration.Container.GetInstance<IConsist>();
                    _consist = _consistService.LoadConsist(filename); 
                    SetOpenConsistOnDataGrid(_consist); 
                    panel_Main.Visible = true;
                }
                catch (Exception ex)
                {
                    GetErrorMessage(ex.Message);
                    panel_Main.Visible = false;
                }
            }

        }

        /// <summary>
        /// Обработчик выбора папки с игрой
        /// </summary>
        private void MenuItem_SetFolders_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }
            

        #endregion

        #region Обработчики событий нажатия на кнопки

        /// <summary>
        /// Обработчик нажатия на кнопку "Добавить локомотив"
        /// </summary>
        private void Button_AddLoco_Click(object sender, EventArgs e)
        {
            var validSelected = listBox_Loco.SelectedItem != null;
            if (validSelected)
            {
                var loco = GetLoco();
                _consistService.AddTrainVehicle(loco);
                SetDataGrid(loco.ModuleConfig, loco.Count, loco.PayloadCoeff);
            }
            else
            {
                GetErrorMessage("Необходимо выбрать локомотив!");
            }

        }

        /// <summary>
        /// Обработчик нажатия на кнопку "Прицепить вагон"
        /// </summary>
        private void Button_AddVagon_Click(object sender, EventArgs e)
        {
            var validSelected = listBox_VagonName.SelectedItem != null;
            if (validSelected)
            {
                var vagon = GetVagon();
                _consistService.AddTrainVehicle(vagon);
                SetDataGrid(vagon.ModuleConfig, vagon.Count, vagon.PayloadCoeff);
            }
            else
            {
                GetErrorMessage("Необходимо выбрать вагон!");
            }

        }

        /// <summary>
        /// Обработчик нажатия на кнопку "Сохранить состав"
        /// </summary>
        private void Button_Serialize_Click(object sender, EventArgs e)
        {
            CreateConsistOption();

            var saveDialog = new SaveFileDialog()
            {
                InitialDirectory = pathTrains,
                Filter = "xml файлы (*.xml)|*.xml"
            };
            if (saveDialog.ShowDialog() == DialogResult.OK) 
            {
                var file = saveDialog.FileName;
                try
                {
                    _consistService.SaveConsist(file);
                    GetOkMessage("Состав успешно сформирован!");
                    
                }
                catch (Exception ex)
                {
                    GetErrorMessage(ex.Message);
                }
                
            }
            
            
        }

        /// <summary>
        /// Метод удаляет единицу подвижного состава из списка подвижного состава поезда
        /// </summary>
        private void Button_DeleteVehecle_Click(object sender, EventArgs e)
        {
            if (!dataGridView_Consists.Rows[0].IsNewRow)
            {

                var index = dataGridView_Consists.CurrentCell.RowIndex;
                try
                {
                    _consistService.RemoveTrainVehicle(index);

                    dataGridView_Consists.Rows.RemoveAt(dataGridView_Consists.CurrentCell.RowIndex);

                    button_DeleteVehecle.Enabled = !dataGridView_Consists.Rows[0].IsNewRow;
                    button_Serialize.Enabled = !dataGridView_Consists.Rows[0].IsNewRow;
                    groupBox_Consist.Enabled = !dataGridView_Consists.Rows[0].IsNewRow;
                }
                catch (Exception ex)
                {
                    GetErrorMessage(ex.Message);
                }
            }
        }

        /// <summary>
        /// Метод обработки нажатия кнопки "Заменить"
        /// Метод позволяет заменить выбранный в DataGrid вагон или локомотив целиком
        /// </summary>
        private void Button_Change_Click(object sender, EventArgs e)
        {
            string module;
            string moduleCfg;

            if (listBox_Loco.SelectedItems.Count != 0)
            {
                var loco = GetLoco();
                module = loco.Module;
                moduleCfg = loco.ModuleConfig;
            }
            else if (listBox_VagonName.SelectedItems.Count != 0)
            {
                var vagon = GetVagon();
                module = vagon.Module;
                moduleCfg = vagon.ModuleConfig;
            }
            else
                return;
            
            DataGridView_Consists_ChangeVehicle(module, moduleCfg);
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Очистить"
        /// </summary>
        private void Button_Clear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        /// <summary>
        /// Обработчик нажатия "Крестика" на форме
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Вызываем окно подтверждения выхода
            if (MessageBox.Show("Вся не сохраненная работа будет утеряна!\r\n Вы желаете выйти из приложения?", 
                                "Внимание!", 
                                MessageBoxButtons.YesNo, 
                                MessageBoxIcon.Exclamation) == DialogResult.No)
                e.Cancel = true;
            else
                e.Cancel = false;
        }


        #endregion

        #region Вспомогательные методы

        /// <summary>
        /// Метод вызова MessageBox сообщающий об ошибке
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        private static void GetErrorMessage(string message)
        {
            MessageBox.Show(message, "Что-то пошло не так!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void GetAttentionMessage(string message)
        {
            MessageBox.Show(message, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Метод вызова MessageBox сообщающий о выполнении действия
        /// </summary>
        /// <param name="message">Отображаемый текст</param>
        private static void GetOkMessage(string message)
        {
            MessageBox.Show(message, "Ура!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Метод вызывающий начальные настройки директорий
        /// </summary>
        private void LoadSettings()
        {

            try
            {
                string result = _settingService.GetPathRRSTrains();
                pathTrains = result;

                InitialListBox();
                MenuItem_CreateNewConsist.Enabled = true;
                MenuItem_OpenConsist.Enabled = true;
                MenuItem_SetFolders.Enabled = false;
            }
            catch (Exception ex)
            {
                GetAttentionMessage(ex.Message);
                GetAttentionMessage("Для работы с программой необходимо указать корректный путь к RRS");
                MenuItem_CreateNewConsist.Enabled = false;
                MenuItem_OpenConsist.Enabled = false;
            }
            

        }

        /// <summary>
        /// Метод позволяющий пользователю выбрать каталог с игрой и записать его в конфигурационный файл
        /// </summary>
        private void SaveSettings()
        {
            var folder = new FolderBrowserDialog();
            if(!folder.ShowDialog().Equals(DialogResult.OK))
                GetAttentionMessage("Убедитесь, что RRS установлен или правильно укажите каталог с игрой");
            else
            {
                var path = folder.SelectedPath;
                if (Directory.Exists(path + @"/cfg"))
                {
                    _settingService.SetPathRRS(path);
                    LoadSettings();
                }
                else
                {
                    GetErrorMessage("Убедитесь, что RRS установлен или правильно укажите каталог с игрой");
                }
            }
        }

        /// <summary>
        /// Метод для получения Данных локомотива из элементов формы
        /// </summary>
        /// <returns>Возвращает кортеж </returns>
        private TrainVehicle GetLoco()
        {
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
            return GetVehicle(module, moduleCfg, locoCount, payloadCoeff);

        }
        /// <summary>
        /// Метод для получения данных вагона из элементов формы
        /// </summary>
        /// <returns>Возвращает кортеж </returns>
        private TrainVehicle GetVagon()
        {
            //Получаем значения параметров
            var module = "passcar";
            var moduleCfg = listBox_VagonName.SelectedItem.ToString();
            int vagonCount = (int.TryParse(textBox_VagonCount.Text, out int count) && count != 0) ? count : 1;
            var payloadCoeff = (double.TryParse(textBox_Coeff.Text, out double coeff) && coeff <= 1.0 && coeff >= 0) ? coeff : 1.0;
            //Убираем маркер выделения
            listBox_VagonName.ClearSelected();

            return GetVehicle(module, moduleCfg, vagonCount, payloadCoeff);
        }
        private static TrainVehicle GetVehicle(string module, string moduleCfg, int count, double payloadCoeff)
        {
            TrainVehicle vehicle = new TrainVehicle
            {
                Module = module,
                ModuleConfig = moduleCfg,
                Count = count,
                PayloadCoeff = payloadCoeff
            };

            return vehicle;
        }
        
        /// <summary>
        /// Метод передает в модель свойств и характеристик поезда данные введенные пользователем
        /// </summary>
        private void CreateConsistOption()
        {
            var title = textBox_ConsistName.Text == "" ? "Поезд" : textBox_ConsistName.Text;
            var descr = textBox_Description.Text == "" ? title : textBox_Description.Text;

            //Поля характеристик поезда.
            //В общем случае заполнены значениями по умолчанию рекомендованными разработчиками игры
            var coupType = listBox_CouplingType.SelectedItem == null ? "ef-coupling" : listBox_CouplingType.SelectedItem.ToString();
            var cabine = textBox_CabineInVehicle.Text == "" ? 0 : int.Parse(textBox_CabineInVehicle.Text);
            var charginPress = textBox_ChargingPressure.Text == "" ? 0.5 : double.Parse(textBox_ChargingPressure.Text);
            var initPress = textBox_InitMainResPressure.Text == "" ? 0.9 : double.Parse(textBox_InitMainResPressure.Text);
            var noAir = checkBox_NoAir.Checked ? 1 : 0;

            TrainConsistInfo consistInfo = new TrainConsistInfo
            {
                Title = title,
                Description = descr,
                CouplingModule = coupType,
                CabineInVehicle = cabine,
                ChargingPressure = charginPress,
                InitMainResPressure = initPress,
                NoAir = noAir
            };

            _consistService.AddConsistOptions(consistInfo);
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
                listBox_CouplingType.SelectedItem = null;
                textBox_ChargingPressure.Text = "";
                textBox_InitMainResPressure.Text = "";
                checkBox_NoAir.Checked = false;

            }
            else
            {
                groupBox_TrainOptons.Visible = true;
            }
        }

        /// <summary>
        /// Метод очищает форму
        /// Блокирует кнопки управления
        /// </summary>
        private void Clear()
        {
            textBox_ConsistName.Clear();
            textBox_Description.Clear();
            dataGridView_Consists.Rows.Clear();
            button_DeleteVehecle.Enabled = false;
            button_Serialize.Enabled = false;
            button_Clear.Enabled = false;
            button_Change.Enabled = false;
        }


        #endregion

        #region ListBox

        /// <summary>
        /// Начальная инициализация UI
        /// </summary>
        private void InitialListBox()
        {
            var listData = _settingService.GetListData();
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

        #region DataGrid


        /// <summary>
        /// Вспомогательный метод управления dataGridView_Consists и кнопками
        /// </summary>
        /// <param name="moduleCfg">Наименование</param>
        /// <param name="count">Количество</param>
        private void SetDataGrid(string moduleCfg, int count, double coeff)
        {
            groupBox_Consist.Enabled = true;

            dataGridView_Consists.Rows.Add(moduleCfg, count, coeff);

            button_Serialize.Enabled = true;
            button_DeleteVehecle.Enabled = true;
            button_Change.Enabled = true;
            button_Clear.Enabled = true;

        }

        /// <summary>
        /// Обработчик редактирования значения ячейки "количество" в DataGridView_Consists
        /// </summary>
        private void DataGridView_Consists_CellAndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!dataGridView_Consists.Rows[0].IsNewRow)
            {
                var index = dataGridView_Consists.CurrentCell.RowIndex;
                
                var column = dataGridView_Consists.CurrentCell.ColumnIndex;
                if (column == 1) //Колонка с количеством вагонов
                {
                    _ = int.TryParse(dataGridView_Consists.CurrentCell.Value.ToString(), out int valueCount);
                    var value = valueCount > 0 ? valueCount : 1;
                    try
                    {
                        _consistService.EditTrainVehicle(index, value);
                        dataGridView_Consists.CurrentCell.Value = value;
                    }
                    catch (Exception ex)
                    {
                        GetErrorMessage(ex.Message);
                    }
                     
                }
                else //Колонка с коэф.загрузки
                {
                    //Получаем количество выбранной пользователем единицы подвижного состава
                    _ = double.TryParse(dataGridView_Consists.CurrentCell.Value.ToString(), out double valueCoef);
                    var value = valueCoef > 0 && valueCoef <= 1.0 ? valueCoef : 1.0;
                    //Вызываем перегрузку метод редактирования коэфф. загруженности подвижного состава
                    try
                    {
                        _consistService.EditTrainVehicle(index, value);
                        dataGridView_Consists.CurrentCell.Value = value;
                    }
                    catch (Exception ex)
                    {
                        GetErrorMessage(ex.Message);
                    }                
                }   
            }
        }

        /// <summary>
        /// Обработчик замены локомотива или вагона в DataGridView_Consists
        /// </summary>
        private void DataGridView_Consists_ChangeVehicle(string module, string moduleCfg)
        {
            //Проверяем есть ли данные в dataGridView_Consists
            if (!dataGridView_Consists.Rows[0].IsNewRow)
            {
                var index = dataGridView_Consists.CurrentCell.RowIndex;

                try
                {
                    _consistService.EditTrainVehicle(index, module, moduleCfg);
                    dataGridView_Consists.CurrentCell.Value = moduleCfg;
                }
                catch (Exception ex)
                {
                    GetOkMessage(ex.Message);
                }
            }
        }

        /// <summary>
        /// Метод устанавливает значения из открытого файла состава в DataGrid и поля формы
        /// </summary>
        /// <param name="model">Модель состава</param>
        private void SetOpenConsistOnDataGrid(IConsist model)
        {
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
                checkBox_NoAir.Checked = model.Common.NoAir == 0;
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
