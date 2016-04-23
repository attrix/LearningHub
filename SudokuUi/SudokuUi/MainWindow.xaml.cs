using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SudokuUi
{
    public class sudokuelem : IEquatable<sudokuelem>
    {
        public int x, y;
        public int value;


        public sudokuelem(int value)
        {
            this.x = 0;
            this.y = 0;
            this.value = value;
        }

        public sudokuelem(int x, int y, int value)
        {
            this.x = x;
            this.y = y;
            this.value = value;
        }

        public bool Equals(sudokuelem other)
        {
            if (other == null)
                return false;

            if (other.value == this.value)
                return true;

            return false;
        }
    }

    class BoxData: INotifyPropertyChanged
    {
        public int boxVal;

        public BoxData()
        {
            boxVal = 0;
        }

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<sudokuelem> elemList = new List<sudokuelem>();
        List<int> possibilitiesLeft = new List<int>();
        BoxData boxData = new BoxData(); 
        public bool ValidSudokuBox()
        {
            bool IsValid = true;
            elemList.ForEach(elem =>
            {
                if (IsValid == false)
                    return;

                sudokuelem elemRow = elemList.Find(x => x.x == elem.x && x.y != elem.y && elem.value == x.value && x.value != 0);
                sudokuelem elemCol = elemList.Find(x => x.y == elem.y && x.x != elem.x && elem.value == x.value && x.value != 0);
                sudokuelem elemBox =
                    elemList.Find(x =>
                        (x.x != elem.x && x.y != elem.y) && 
                        (x.x/3 == elem.x/3 && x.y/3 == elem.y/3) && 
                            (x.value == elem.value && x.value != 0));

                if (elemCol != null || elemRow != null || elemBox != null)
                {
                    IsValid = false;
                    return;
                }
            });
            return IsValid;
        }
        public bool SolveSudoku()
        {
            sudokuelem nextElem = elemList.Find(x => x.value == 0);
            List<int> tempElemList = new List<int>() {1, 2, 3, 4, 5, 6, 7, 8, 9};
            bool IsSolved = false;            

            if (!ValidSudokuBox())
                return false;

            // All filed up
            if (nextElem == null)
                return true;

            elemList.ForEach(x =>
            {
                if ((x.value != 0) && (x.x == nextElem.x || x.y == nextElem.y ||
                    (x.x/3 == nextElem.x/3 && x.y/3 == nextElem.y/3)))
                {
                    tempElemList.Remove(x.value);
                }
            });

            tempElemList.ForEach(elem =>
            {
                if (IsSolved == false)
                {
                    nextElem.value = elem;
                    IsSolved = SolveSudoku();
                    if (IsSolved == false)
                    {
                        nextElem.value = 0;
                    }
                }
            });

            nextElem = elemList.Find(x => x.value == 0);

            // All filed up
            if (nextElem == null)
                return true;
            else
                return false;
        }

        public MainWindow()
        {
            InitializeComponent();
            this.SudokuGrid.ShowGridLines = true;
            for (int i = 1; i <= 9; i++)
            {
                for (int j = 1; j <= 9; j++)
                {
                    possibilitiesLeft.Add(j);
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InitializeSudoku();

            if (SolveSudoku())
            {
                UpdateSudoku();
            }
        }
        public void InitializeSudoku()
        {
            elemList.RemoveAll(x => x.x != -1);

            int temp = 0;
            Int32.TryParse(box00.Text, out temp);
            elemList.Add(new sudokuelem(0, 0, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box01.Text, out temp);
            elemList.Add(new sudokuelem(0, 1, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box02.Text, out temp);
            elemList.Add(new sudokuelem(0, 2, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box03.Text, out temp);
            elemList.Add(new sudokuelem(0, 3, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box04.Text, out temp);
            elemList.Add(new sudokuelem(0, 4, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box05.Text, out temp);
            elemList.Add(new sudokuelem(0, 5, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box06.Text, out temp);
            elemList.Add(new sudokuelem(0, 6, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box07.Text, out temp);
            elemList.Add(new sudokuelem(0, 7, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box08.Text, out temp);
            elemList.Add(new sudokuelem(0, 8, temp));
            possibilitiesLeft.Remove(temp);

            temp = 0;
            Int32.TryParse(box10.Text, out temp);
            elemList.Add(new sudokuelem(1, 0, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box11.Text, out temp);
            elemList.Add(new sudokuelem(1, 1, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box12.Text, out temp);
            elemList.Add(new sudokuelem(1, 2, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box13.Text, out temp);
            elemList.Add(new sudokuelem(1, 3, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box14.Text, out temp);
            elemList.Add(new sudokuelem(1, 4, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box15.Text, out temp);
            elemList.Add(new sudokuelem(1, 5, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box16.Text, out temp);
            elemList.Add(new sudokuelem(1, 6, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box17.Text, out temp);
            elemList.Add(new sudokuelem(1, 7, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box18.Text, out temp);
            elemList.Add(new sudokuelem(1, 8, temp));
            possibilitiesLeft.Remove(temp);

            temp = 0;
            Int32.TryParse(box20.Text, out temp);
            elemList.Add(new sudokuelem(2, 0, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box21.Text, out temp);
            elemList.Add(new sudokuelem(2, 1, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box22.Text, out temp);
            elemList.Add(new sudokuelem(2, 2, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box23.Text, out temp);
            elemList.Add(new sudokuelem(2, 3, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box24.Text, out temp);
            elemList.Add(new sudokuelem(2, 4, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box25.Text, out temp);
            elemList.Add(new sudokuelem(2, 5, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box26.Text, out temp);
            elemList.Add(new sudokuelem(2, 6, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box27.Text, out temp);
            elemList.Add(new sudokuelem(2, 7, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box28.Text, out temp);
            elemList.Add(new sudokuelem(2, 8, temp));
            possibilitiesLeft.Remove(temp);

            temp = 0;
            Int32.TryParse(box30.Text, out temp);
            elemList.Add(new sudokuelem(3, 0, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box31.Text, out temp);
            elemList.Add(new sudokuelem(3, 1, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box32.Text, out temp);
            elemList.Add(new sudokuelem(3, 2, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box33.Text, out temp);
            elemList.Add(new sudokuelem(3, 3, temp));
            temp = 0;
            Int32.TryParse(box34.Text, out temp);
            elemList.Add(new sudokuelem(3, 4, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box35.Text, out temp);
            elemList.Add(new sudokuelem(3, 5, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box36.Text, out temp);
            elemList.Add(new sudokuelem(3, 6, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box37.Text, out temp);
            elemList.Add(new sudokuelem(3, 7, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box38.Text, out temp);
            elemList.Add(new sudokuelem(3, 8, temp));
            possibilitiesLeft.Remove(temp);

            temp = 0;
            Int32.TryParse(box40.Text, out temp);
            elemList.Add(new sudokuelem(4, 0, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box41.Text, out temp);
            elemList.Add(new sudokuelem(4, 1, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box42.Text, out temp);
            elemList.Add(new sudokuelem(4, 2, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box43.Text, out temp);
            elemList.Add(new sudokuelem(4, 3, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box44.Text, out temp);
            elemList.Add(new sudokuelem(4, 4, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box45.Text, out temp);
            elemList.Add(new sudokuelem(4, 5, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box46.Text, out temp);
            elemList.Add(new sudokuelem(4, 6, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box47.Text, out temp);
            elemList.Add(new sudokuelem(4, 7, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box48.Text, out temp);
            elemList.Add(new sudokuelem(4, 8, temp));
            possibilitiesLeft.Remove(temp);

            temp = 0;
            Int32.TryParse(box50.Text, out temp);
            elemList.Add(new sudokuelem(5, 0, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box51.Text, out temp);
            elemList.Add(new sudokuelem(5, 1, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box52.Text, out temp);
            elemList.Add(new sudokuelem(5, 2, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box53.Text, out temp);
            elemList.Add(new sudokuelem(5, 3, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box54.Text, out temp);
            elemList.Add(new sudokuelem(5, 4, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box55.Text, out temp);
            elemList.Add(new sudokuelem(5, 5, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box56.Text, out temp);
            elemList.Add(new sudokuelem(5, 6, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box57.Text, out temp);
            elemList.Add(new sudokuelem(5, 7, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box58.Text, out temp);
            elemList.Add(new sudokuelem(5, 8, temp));
            possibilitiesLeft.Remove(temp);

            temp = 0;
            Int32.TryParse(box60.Text, out temp);
            elemList.Add(new sudokuelem(6, 0, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box61.Text, out temp);
            elemList.Add(new sudokuelem(6, 1, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box62.Text, out temp);
            elemList.Add(new sudokuelem(6, 2, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box63.Text, out temp);
            elemList.Add(new sudokuelem(6, 3, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box64.Text, out temp);
            elemList.Add(new sudokuelem(6, 4, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box65.Text, out temp);
            elemList.Add(new sudokuelem(6, 5, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box66.Text, out temp);
            elemList.Add(new sudokuelem(6, 6, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box67.Text, out temp);
            elemList.Add(new sudokuelem(6, 7, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box68.Text, out temp);
            elemList.Add(new sudokuelem(6, 8, temp));
            possibilitiesLeft.Remove(temp);

            temp = 0;
            Int32.TryParse(box70.Text, out temp);
            elemList.Add(new sudokuelem(7, 0, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box71.Text, out temp);
            elemList.Add(new sudokuelem(7, 1, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box72.Text, out temp);
            elemList.Add(new sudokuelem(7, 2, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box73.Text, out temp);
            elemList.Add(new sudokuelem(7, 3, temp));
            temp = 0;
            Int32.TryParse(box74.Text, out temp);
            elemList.Add(new sudokuelem(7, 4, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box75.Text, out temp);
            elemList.Add(new sudokuelem(7, 5, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box76.Text, out temp);
            elemList.Add(new sudokuelem(7, 6, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box77.Text, out temp);
            elemList.Add(new sudokuelem(7, 7, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box78.Text, out temp);
            elemList.Add(new sudokuelem(7, 8, temp));
            possibilitiesLeft.Remove(temp);

            temp = 0;
            Int32.TryParse(box80.Text, out temp);
            elemList.Add(new sudokuelem(8, 0, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box81.Text, out temp);
            elemList.Add(new sudokuelem(8, 1, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box82.Text, out temp);
            elemList.Add(new sudokuelem(8, 2, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box83.Text, out temp);
            elemList.Add(new sudokuelem(8, 3, temp));
            temp = 0;
            Int32.TryParse(box84.Text, out temp);
            elemList.Add(new sudokuelem(8, 4, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box85.Text, out temp);
            elemList.Add(new sudokuelem(8, 5, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box86.Text, out temp);
            elemList.Add(new sudokuelem(8, 6, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box87.Text, out temp);
            elemList.Add(new sudokuelem(8, 7, temp));
            possibilitiesLeft.Remove(temp);
            temp = 0;
            Int32.TryParse(box88.Text, out temp);
            elemList.Add(new sudokuelem(8, 8, temp));
            possibilitiesLeft.Remove(temp);
        }
        public void UpdateSudoku()
        {
            elemList.ForEach(x =>
            {
                switch (x.x)
                {
                    case 0:
                        switch (x.y)
                        {
                            case 0:
                                box00.Text = x.value.ToString();
                                box00.InvalidateVisual();
                                break;
                            case 1:
                                box01.Text = x.value.ToString();
                                box01.InvalidateVisual();
                                break;
                            case 2:
                                box02.Text = x.value.ToString();
                                box02.InvalidateVisual();
                                break;
                            case 3:
                                box03.Text = x.value.ToString();
                                box03.InvalidateVisual();
                                break;
                            case 4:
                                box04.Text = x.value.ToString();
                                box04.InvalidateVisual();
                                break;
                            case 5:
                                box05.Text = x.value.ToString();
                                box05.InvalidateVisual();
                                break;
                            case 6:
                                box06.Text = x.value.ToString();
                                box06.InvalidateVisual();
                                break;
                            case 7:
                                box07.Text = x.value.ToString();
                                box07.InvalidateVisual();
                                break;
                            case 8:
                                box08.Text = x.value.ToString();
                                box08.InvalidateVisual();
                                break;
                        }
                        break;
                    case 1:
                        switch (x.y)
                        {
                            case 0:
                                box10.Text = x.value.ToString();
                                box10.InvalidateVisual();
                                break;
                            case 1:
                                box11.Text = x.value.ToString();
                                box11.InvalidateVisual();
                                break;
                            case 2:
                                box12.Text = x.value.ToString();
                                box12.InvalidateVisual();
                                break;
                            case 3:
                                box13.Text = x.value.ToString();
                                box13.InvalidateVisual();
                                break;
                            case 4:
                                box14.Text = x.value.ToString();
                                box14.InvalidateVisual();
                                break;
                            case 5:
                                box15.Text = x.value.ToString();
                                box15.InvalidateVisual();
                                break;
                            case 6:
                                box16.Text = x.value.ToString();
                                box16.InvalidateVisual();
                                break;
                            case 7:
                                box17.Text = x.value.ToString();
                                box17.InvalidateVisual();
                                break;
                            case 8:
                                box18.Text = x.value.ToString();
                                box18.InvalidateVisual();
                                break;
                        }
                        break;
                    case 2:
                        switch (x.y)
                        {
                            case 0:
                                box20.Text = x.value.ToString();
                                box20.InvalidateVisual();
                                break;
                            case 1:
                                box21.Text = x.value.ToString();
                                box21.InvalidateVisual();
                                break;
                            case 2:
                                box22.Text = x.value.ToString();
                                box22.InvalidateVisual();
                                break;
                            case 3:
                                box23.Text = x.value.ToString();
                                box23.InvalidateVisual();
                                break;
                            case 4:
                                box24.Text = x.value.ToString();
                                box24.InvalidateVisual();
                                break;
                            case 5:
                                box25.Text = x.value.ToString();
                                box25.InvalidateVisual();
                                break;
                            case 6:
                                box26.Text = x.value.ToString();
                                box26.InvalidateVisual();
                                break;
                            case 7:
                                box27.Text = x.value.ToString();
                                box27.InvalidateVisual();
                                break;
                            case 8:
                                box28.Text = x.value.ToString();
                                box28.InvalidateVisual();
                                break;
                        }
                        break;
                    case 3:
                        switch (x.y)
                        {
                            case 0:
                                box30.Text = x.value.ToString();
                                box30.InvalidateVisual();
                                break;
                            case 1:
                                box31.Text = x.value.ToString();
                                box31.InvalidateVisual();
                                break;
                            case 2:
                                box32.Text = x.value.ToString();
                                box32.InvalidateVisual();
                                break;
                            case 3:
                                box33.Text = x.value.ToString();
                                box33.InvalidateVisual();
                                break;
                            case 4:
                                box34.Text = x.value.ToString();
                                box34.InvalidateVisual();
                                break;
                            case 5:
                                box35.Text = x.value.ToString();
                                box35.InvalidateVisual();
                                break;
                            case 6:
                                box36.Text = x.value.ToString();
                                box36.InvalidateVisual();
                                break;
                            case 7:
                                box37.Text = x.value.ToString();
                                box37.InvalidateVisual();
                                break;
                            case 8:
                                box38.Text = x.value.ToString();
                                box38.InvalidateVisual();
                                break;
                        }
                        break;
                    case 4:
                        switch (x.y)
                        {
                            case 0:
                                box40.Text = x.value.ToString();
                                box40.InvalidateVisual();
                                break;
                            case 1:
                                box41.Text = x.value.ToString();
                                box41.InvalidateVisual();
                                break;
                            case 2:
                                box42.Text = x.value.ToString();
                                box42.InvalidateVisual();
                                break;
                            case 3:
                                box43.Text = x.value.ToString();
                                box43.InvalidateVisual();
                                break;
                            case 4:
                                box44.Text = x.value.ToString();
                                box44.InvalidateVisual();
                                break;
                            case 5:
                                box45.Text = x.value.ToString();
                                box45.InvalidateVisual();
                                break;
                            case 6:
                                box46.Text = x.value.ToString();
                                box46.InvalidateVisual();
                                break;
                            case 7:
                                box47.Text = x.value.ToString();
                                box47.InvalidateVisual();
                                break;
                            case 8:
                                box48.Text = x.value.ToString();
                                box48.InvalidateVisual();
                                break;
                        }
                        break;
                    case 5:
                        switch (x.y)
                        {
                            case 0:
                                box50.Text = x.value.ToString();
                                box50.InvalidateVisual();
                                break;
                            case 1:
                                box51.Text = x.value.ToString();
                                box51.InvalidateVisual();
                                break;
                            case 2:
                                box52.Text = x.value.ToString();
                                box52.InvalidateVisual();
                                break;
                            case 3:
                                box53.Text = x.value.ToString();
                                box53.InvalidateVisual();
                                break;
                            case 4:
                                box54.Text = x.value.ToString();
                                box54.InvalidateVisual();
                                break;
                            case 5:
                                box55.Text = x.value.ToString();
                                box55.InvalidateVisual();
                                break;
                            case 6:
                                box56.Text = x.value.ToString();
                                box56.InvalidateVisual();
                                break;
                            case 7:
                                box57.Text = x.value.ToString();
                                box57.InvalidateVisual();
                                break;
                            case 8:
                                box58.Text = x.value.ToString();
                                box58.InvalidateVisual();
                                break;
                        }
                        break;
                    case 6:
                        switch (x.y)
                        {
                            case 0:
                                box60.Text = x.value.ToString();
                                box60.InvalidateVisual();
                                break;
                            case 1:
                                box61.Text = x.value.ToString();
                                box61.InvalidateVisual();
                                break;
                            case 2:
                                box62.Text = x.value.ToString();
                                box62.InvalidateVisual();
                                break;
                            case 3:
                                box63.Text = x.value.ToString();
                                box63.InvalidateVisual();
                                break;
                            case 4:
                                box64.Text = x.value.ToString();
                                box64.InvalidateVisual();
                                break;
                            case 5:
                                box65.Text = x.value.ToString();
                                box65.InvalidateVisual();
                                break;
                            case 6:
                                box66.Text = x.value.ToString();
                                box66.InvalidateVisual();
                                break;
                            case 7:
                                box67.Text = x.value.ToString();
                                box67.InvalidateVisual();
                                break;
                            case 8:
                                box68.Text = x.value.ToString();
                                box68.InvalidateVisual();
                                break;
                        }
                        break;
                    case 7:
                        switch (x.y)
                        {
                            case 0:
                                box70.Text = x.value.ToString();
                                box70.InvalidateVisual();
                                break;
                            case 1:
                                box71.Text = x.value.ToString();
                                box71.InvalidateVisual();
                                break;
                            case 2:
                                box72.Text = x.value.ToString();
                                box72.InvalidateVisual();
                                break;
                            case 3:
                                box73.Text = x.value.ToString();
                                box73.InvalidateVisual();
                                break;
                            case 4:
                                box74.Text = x.value.ToString();
                                box74.InvalidateVisual();
                                break;
                            case 5:
                                box75.Text = x.value.ToString();
                                box75.InvalidateVisual();
                                break;
                            case 6:
                                box76.Text = x.value.ToString();
                                box76.InvalidateVisual();
                                break;
                            case 7:
                                box77.Text = x.value.ToString();
                                box77.InvalidateVisual();
                                break;
                            case 8:
                                box78.Text = x.value.ToString();
                                box78.InvalidateVisual();
                                break;
                        }
                        break;
                    case 8:
                        switch (x.y)
                        {
                            case 0:
                                box80.Text = x.value.ToString();
                                box80.InvalidateVisual();
                                break;
                            case 1:
                                box81.Text = x.value.ToString();
                                box81.InvalidateVisual();
                                break;
                            case 2:
                                box82.Text = x.value.ToString();
                                box82.InvalidateVisual();
                                break;
                            case 3:
                                box83.Text = x.value.ToString();
                                box83.InvalidateVisual();
                                break;
                            case 4:
                                box84.Text = x.value.ToString();
                                box84.InvalidateVisual();
                                break;
                            case 5:
                                box85.Text = x.value.ToString();
                                box85.InvalidateVisual();
                                break;
                            case 6:
                                box86.Text = x.value.ToString();
                                box86.InvalidateVisual();
                                break;
                            case 7:
                                box87.Text = x.value.ToString();
                                box87.InvalidateVisual();
                                break;
                            case 8:
                                box88.Text = x.value.ToString();
                                box88.InvalidateVisual();
                                break;
                        }
                        break;
                }
            });
        }

    }
}
