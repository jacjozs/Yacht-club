using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Yacht_club.Moduls
{
    /// <summary>
    /// Interaction logic for AutoTextBoxScroll.xaml
    /// Használt projekt
    /// https://www.codeproject.com/Articles/26535/WPF-Autocomplete-Textbox-Control
    /// </summary>
    public partial class AutoTextBoxScroll : Canvas
    {
        #region Members
        private VisualCollection controls;
        private TextBox textBox;
        public ComboBox comboBox;
        private ObservableCollection<AutoCompleteEntry> autoCompletionList;
        private System.Timers.Timer keypressTimer;
        private delegate void TextChangedCallback();
        private bool insertText;
        public int delayTime;
        public int ID;
        private int searchThreshold;
        #endregion

        #region Constructor
        public AutoTextBoxScroll()
        {
            controls = new VisualCollection(this);
            InitializeComponent();

            autoCompletionList = new ObservableCollection<AutoCompleteEntry>();
            searchThreshold = 2;        // default threshold to 2 char

            // set up the key press timer
            keypressTimer = new System.Timers.Timer();
            keypressTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);

            // set up the text box and the combo box
            comboBox = new ComboBox();
            comboBox.IsSynchronizedWithCurrentItem = true;
            comboBox.IsTabStop = false;
            comboBox.SelectionChanged += new SelectionChangedEventHandler(comboBox_SelectionChanged);
            

            textBox = new TextBox();
            textBox.TextChanged += new TextChangedEventHandler(textBox_TextChanged);
            textBox.VerticalContentAlignment = VerticalAlignment.Center;
            textBox.Style = Application.Current.FindResource("TextBoxStyle") as Style;

            controls.Add(comboBox);
            controls.Add(textBox);
        }
        #endregion

        #region Methods
        public string Text
        {
            get { return textBox.Text; }
            set
            {
                insertText = true;
                textBox.Text = value;
            }
        }

        public int DelayTime
        {
            get { return delayTime; }
            set { delayTime = value; }
        }

        public int Threshold
        {
            get { return searchThreshold; }
            set { searchThreshold = value; }
        }

        public void AddItem(AutoCompleteEntry entry)
        {
            autoCompletionList.Add(entry);
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (null != comboBox.SelectedItem)
            {
                insertText = true;
                ComboBoxItem cbItem = (ComboBoxItem)comboBox.SelectedItem;
                textBox.Text = cbItem.Content.ToString();
                ID = int.Parse(cbItem.Uid);
            }
        }

        private void TextChanged()
        {
            try
            {
                comboBox.Items.Clear();
                if (textBox.Text.Length >= searchThreshold)
                {
                    foreach (AutoCompleteEntry entry in autoCompletionList)
                    {
                        foreach (string word in entry.KeywordStrings)
                        {
                            if (word.StartsWith(textBox.Text, StringComparison.CurrentCultureIgnoreCase))
                            {
                                ComboBoxItem cbItem = new ComboBoxItem();
                                cbItem.Style = (Style)Application.Current.FindResource("ComboBoxTextStyle");
                                cbItem.Uid = entry.Id.ToString();
                                cbItem.Content = entry.ToString();
                                comboBox.Items.Add(cbItem);
                                break;
                            }
                        }
                    }
                    comboBox.IsDropDownOpen = comboBox.HasItems;
                }
                else
                {
                    comboBox.IsDropDownOpen = false;
                }
            }
            catch { }
        }

        private void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
            keypressTimer.Stop();
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                new TextChangedCallback(this.TextChanged));
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // text was not typed, do nothing and consume the flag
            if (insertText == true) insertText = false;

            // if the delay time is set, delay handling of text changed
            else
            {
                if (delayTime > 0)
                {
                    keypressTimer.Interval = delayTime;
                    keypressTimer.Start();
                }
                else TextChanged();
            }
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            textBox.Arrange(new Rect(arrangeSize));
            comboBox.Arrange(new Rect(arrangeSize));
            return base.ArrangeOverride(arrangeSize);
        }

        protected override Visual GetVisualChild(int index)
        {
            return controls[index];
        }

        protected override int VisualChildrenCount
        {
            get { return controls.Count; }
        }
        #endregion
    }
}
