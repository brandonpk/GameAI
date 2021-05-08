using System;
using System.Collections.Generic;
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

namespace MediumEight
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //initialize all the variables that will be used in almost every method that way they do not have to be sent through
        //the passed variables of the methods
        #region Private Members
        private MarkType[] mResults;
        private bool mPlayer1Turn;
        private bool mGameEnded;
        private int moveCounter;
        private RoutedEventArgs a;
        private int[] madeMoves;
        private string computer;
        private string person;
        private int resultCoin;


        #endregion

        #region Constructor
        //default constructor
        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }

        #endregion

        private void FlipCoin()
        {
            //this will randomly chooses a digit of one or two and determine who goes first in the game
            Random coin = new Random();
            resultCoin = coin.Next(1, 3);

            switch (resultCoin)
            {
                case 1:
                    MakeMove();
                    break;
                default:
                    break;
            }//switch
        }//flip coin

        private void NewGame()
        {
            //create array of open spots
            mResults = new MarkType[15];
            //reset move counter
            moveCounter = 0;
            //fill array with empty variables
            for (var k = 0; k < mResults.Length; k++)
                mResults[k] = MarkType.Free;

            mPlayer1Turn = true;

            #region ClearBoard

            Button0_0.Content = string.Empty; Button0_0.Background = Brushes.White; Button0_0.Foreground = Brushes.Blue;
            Button0_1.Content = string.Empty; Button0_1.Background = Brushes.White; Button0_1.Foreground = Brushes.Blue;
            Button0_2.Content = string.Empty; Button0_2.Background = Brushes.White; Button0_2.Foreground = Brushes.Blue;
            Button1_0.Content = string.Empty; Button1_0.Background = Brushes.White; Button1_0.Foreground = Brushes.Blue;
            Button2_0.Content = string.Empty; Button2_0.Background = Brushes.White; Button2_0.Foreground = Brushes.Blue;
            Button1_1.Content = string.Empty; Button1_1.Background = Brushes.White; Button1_1.Foreground = Brushes.Blue;
            Button2_2.Content = string.Empty; Button2_2.Background = Brushes.White; Button2_2.Foreground = Brushes.Blue;
            Button2_1.Content = string.Empty; Button2_1.Background = Brushes.White; Button2_1.Foreground = Brushes.Blue;
            Button1_2.Content = string.Empty; Button1_2.Background = Brushes.White; Button1_2.Foreground = Brushes.Blue;

            Output.Content = "Player X's Turn"; Output.Background = Brushes.Yellow;

            #endregion

            mGameEnded = false;
            //if I want to be able to track which moves have been made this will allow me to do so
            madeMoves = new int[] { 6, 7, 8, 9, 10, 11, 12, 13, 14 };

            FlipCoin();

            //set a variable depending if the coin toss is 1 or 0
            if(resultCoin == 1)
            {
                computer = "Cross";
                person = "Nought";
            }
            else
            {
                computer = "Nought";
                person = "Cross";
            }
        }//new game method

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //if game has ended return out of method to now allow any other clicks
            if (mGameEnded)
                return;
            
            var button = (Button)sender;

            //find position
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);
            var index = column + (row * 3);

            //if position is free then increase movecounter if not return out of method not doing anything
            if (mResults[index] != MarkType.Free)
                return;
            moveCounter++;

            //set the spot value depending on player
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;

            button.Content = mPlayer1Turn ? "X" : "O";


            bool turn = mPlayer1Turn;

            if (!turn)
            {
                Output.Content = "Player O's Turn"; Output.Background = Brushes.Yellow;

            }
            else
            {
                Output.Content = "Player X's Turn"; Output.Background = Brushes.Yellow;

            }

            //change the player turn
            mPlayer1Turn ^= true;

            //change button foreground 
            button.Foreground = Brushes.Red;

            CheckForWinner();

            //initialize variables used in the following cases
            bool third = false;
            bool first = false;
            bool second = false;
            bool w = false;
            bool fifth = false;
            bool fourth = false;
            if (computer.Equals("Nought"))
                w = true;

            //check what the move counter is at that way we can check the cases for where the board is at
            switch (moveCounter)
            {
                case 1: first = true; break;
                case 2: second = true; break;
                case 3: third = true; break;
                case 4: fourth = true; break;
                case 5: fifth = true; break;
            }
            
            //these will avoid any possible traps the player can make
            //these will also place traps if possible
            if (first &&  w)
            {
                if(mResults[10] == MarkType.Free)
                {
                    moveCounter++;
                    AIMakesMove(10);
                    mPlayer1Turn ^= true;
                }
                else if (mResults[10] == MarkType.Cross)
                {
                    moveCounter++;
                    AIMakesMove(6);
                    mPlayer1Turn ^= true;
                }
                else if (mResults[13] == MarkType.Cross)
                {
                    moveCounter++;
                    AIMakesMove(10);
                    mPlayer1Turn ^= true;
                }
                else if ( mResults[11] == MarkType.Cross)
                {
                    moveCounter++;
                    AIMakesMove(10);
                    mPlayer1Turn ^= true;
                }
                else if(mResults[14] == MarkType.Cross)
                {
                    moveCounter++;
                    AIMakesMove(10);
                    mPlayer1Turn ^= true;
                }
                else
                {
                    MakeMove();
                }
            }// first if
            else if (second)
            {
                if (mResults[10] == MarkType.Nought)
                {
                    if(mResults[6] == MarkType.Cross)
                    {
                        moveCounter++;
                        AIMakesMove(14);
                        mPlayer1Turn ^= true;
                    }
                }
                else if (mResults[6] == MarkType.Cross)
                    if(mResults[10] == MarkType.Nought)
                    {
                        MakeMove();
                    }
                else if (mResults[13] == MarkType.Nought)
                {
                    moveCounter++;
                    AIMakesMove(12);
                    mPlayer1Turn ^= true;
                }
                else if (mResults[11] == MarkType.Nought)
                {
                    moveCounter++;
                    AIMakesMove(8);
                    mPlayer1Turn ^= true;
                }
                else if(mResults[14] == MarkType.Nought)
                {
                    moveCounter++;
                    AIMakesMove(8);
                    mPlayer1Turn ^= true;
                }
                else if ((mResults[7] == MarkType.Nought) || mResults[8] == MarkType.Nought)
                {
                    moveCounter++;
                    AIMakesMove(12);
                    mPlayer1Turn ^= true;
                }
                else if (mResults[9] == MarkType.Nought)
                {
                    moveCounter++;
                    AIMakesMove(8);
                    mPlayer1Turn ^= true;
                }
                else if (mResults[12] == MarkType.Nought)
                {
                    moveCounter++;
                    AIMakesMove(8);
                    mPlayer1Turn ^= true;
                }
                else if(mResults[6] == MarkType.Cross)
                {
                    if(mResults[8] == MarkType.Nought)
                    {
                        moveCounter++;
                        AIMakesMove(12);
                        mPlayer1Turn ^= true;
                    }
                }
                
                else
                {
                    MakeMove();
                }
            }//second if
            else if (third && w)
            {


                if (mResults[9] == MarkType.Cross)
                {
                    if (mResults[7] == MarkType.Cross)
                    {
                        moveCounter++;
                        AIMakesMove(6);
                        mPlayer1Turn ^= true;
                    }
                    else if (mResults[13] == MarkType.Cross)
                    {
                        moveCounter++;
                        AIMakesMove(12);
                        mPlayer1Turn ^= true;
                    }
                    else if(mResults[8] == MarkType.Cross)
                    {
                        moveCounter++;
                        AIMakesMove(13);
                        mPlayer1Turn ^= true;
                    }
                    else if (mResults[14] == MarkType.Cross)
                    {
                        moveCounter++;
                        AIMakesMove(7);
                        mPlayer1Turn ^= true;
                    }
                    else
                    {
                        MakeMove();
                    }
                }//9
                else if (mResults[11] == MarkType.Cross)
                {
                    if (mResults[7] == MarkType.Cross)
                    {
                        moveCounter++;
                        AIMakesMove(8);
                        mPlayer1Turn ^= true;
                    }
                    else if (mResults[13] == MarkType.Cross)
                    {
                        moveCounter++;
                        AIMakesMove(14);
                        mPlayer1Turn ^= true;
                    }
                    else if (mResults[6] == MarkType.Cross)
                    {
                        moveCounter++;
                        AIMakesMove(13);
                        mPlayer1Turn ^= true;
                    }
                    else if (mResults[12] == MarkType.Cross)
                    {
                        moveCounter++;
                        AIMakesMove(7);
                        mPlayer1Turn ^= true;
                    }
                    else
                    {
                        MakeMove();
                    }
                }// 11 7, 11 13if (moveCounter == 3)
                else if(mResults[8] == MarkType.Cross)
                {
                    if(mResults[12] == MarkType.Cross)
                    {
                        moveCounter++;
                        AIMakesMove(7);
                        mPlayer1Turn ^= true;
                    }
                    else if (mResults[9] == MarkType.Cross)
                    {
                        moveCounter++;
                        AIMakesMove(13);
                        mPlayer1Turn ^= true;
                    }
                    else if (mResults[13] == MarkType.Cross)
                    {
                        moveCounter++;
                        AIMakesMove(9);
                        mPlayer1Turn ^= true;
                    }
                    else MakeMove();
                }
                else if (mResults[6] == MarkType.Cross)
                {
                    if (mResults[14] == MarkType.Cross)
                    {
                        moveCounter++;
                        AIMakesMove(7);
                        mPlayer1Turn ^= true;
                    }
                    else if (mResults[13] == MarkType.Cross)
                    {
                        moveCounter++;
                        AIMakesMove(11);
                        mPlayer1Turn ^= true;
                    }
                    else MakeMove();
                }
                else if (mResults[10] == MarkType.Cross)
                {
                    if (mResults[14] == MarkType.Cross)
                    {
                        moveCounter++;
                        AIMakesMove(8);
                        mPlayer1Turn ^= true;
                    }
                    else MakeMove();
                }
                else if (mResults[7] == MarkType.Cross)
                {
                    if (mResults[12] == MarkType.Cross)
                    {
                        moveCounter++;
                        AIMakesMove(11);
                        mPlayer1Turn ^= true;
                    }
                    else if (mResults[14] == MarkType.Cross)
                    {
                        moveCounter++;
                        AIMakesMove(9);
                        mPlayer1Turn ^= true;
                    }
                    else MakeMove();
                }
                else if (mResults[13] == MarkType.Cross)
                {
                    if (mResults[6] == MarkType.Cross)
                    {
                        moveCounter++;
                        AIMakesMove(11);
                        mPlayer1Turn ^= true;
                    }
                    else if (mResults[8] == MarkType.Cross)
                    {
                        moveCounter++;
                        AIMakesMove(9);
                        mPlayer1Turn ^= true;
                    }
                    else MakeMove();
                }
                else if(mResults[9] == MarkType.Cross)
                {
                    if(mResults[8] == MarkType.Cross)
                    {
                        moveCounter++;
                        AIMakesMove(6);
                        mPlayer1Turn ^= true;
                    }
                    else if(mResults[14] == MarkType.Cross)
                    {
                        moveCounter++;
                        AIMakesMove(13);
                        mPlayer1Turn ^= true;
                    }
                }
                else
                {
                    MakeMove();
                }
                
            }
            else if (fourth && !w)  
            {
                if (CheckForWin() != 0)
                {
                    MakeMove();
                }
                else if (mResults[6] == MarkType.Cross && (mResults[6] & mResults[12]) == mResults[6])
                {
                    if (mResults[8] == MarkType.Nought)
                    {
                        moveCounter++;
                        AIMakesMove(14);
                        mPlayer1Turn ^= true;
                    }
                    else MakeMove();
                }
                else if (mResults[6] == MarkType.Cross && (mResults[6] & mResults[8]) == mResults[6])
                {
                    if (mResults[12] == MarkType.Nought)
                    {
                        if(mResults[14] == MarkType.Free)
                        {
                            moveCounter++;
                            AIMakesMove(14);
                            mPlayer1Turn ^= true;
                        }
                    }
                    else if(mResults[14] == MarkType.Nought &&(mResults[14] & mResults[7]) == mResults[14])
                    {
                        moveCounter++;
                        AIMakesMove(12);
                        mPlayer1Turn ^= true;
                    }
                    else MakeMove();
                }
                else MakeMove();
            }
            else if (fifth && w)
            {
                if (CheckForWin() != 0)
                {
                    MakeMove();
                }
                else if (mResults[10] == MarkType.Cross)
                {
                    if (mResults[14] == MarkType.Cross)
                    {
                        if (mResults[7] == MarkType.Cross)
                        {
                            moveCounter++;
                            AIMakesMove(13);
                            mPlayer1Turn ^= true;
                        }
                        else if (mResults[9] == MarkType.Cross)
                        {
                            moveCounter++;
                            AIMakesMove(11);
                            mPlayer1Turn ^= true;
                        }
                        else
                        {
                            MakeMove();
                        }
                    }
                    else MakeMove();
                }
                else MakeMove();
            }
           


            else MakeMove();

        }//button function

        private void MakeMove()
        {
            //here is where the AI will make a move

            //if game has ended return 
            if (mGameEnded)
                return;
            int[] arrayToCheck = new int[9] { 6, 7, 8, 9, 10, 11, 12, 13, 14 };

            //check if tie game if not then generate random number 
            //then check if there are winning positions
            //finally check if there are losses and if not send that value to AIMakesMove()
            if (moveCounter != 9)
            {

                int random = 0;
                while (true)
                {
                    
                    Random rnd = new Random();
                    random = rnd.Next(7, 15);
                    if (mResults[random] == MarkType.Free)
                    {
                        break;
                    }//if
                }

                int z = CheckForWin();
                if(z == 0)
                {
                    AIMakesMove(CheckForLoss(random));
                }
                else
                {
                    AIMakesMove(z);
                }


                mPlayer1Turn ^= true;
            }
            moveCounter++;

            //once out of that if^ increment the movecounter as a move has been made then check for a winner
            CheckForWinner();


        }

        private int CheckForWin()
        {
            //check for any possible wins in the rows, columns, diagnols or possible traps
            #region trap
           

            if (mResults[8].ToString() == person && (mResults[8] & mResults[12]) == mResults[8])
            {
                if (moveCounter == 3)
                {

                }
            }
            #endregion

            #region HoriCheck
            if (mResults[6].ToString() == computer && (mResults[6] & mResults[7]) == mResults[6])
            {
                if (mResults[8] == MarkType.Free) return 8;
            }
            if (mResults[7].ToString() == computer && (mResults[7] & mResults[8]) == mResults[7])
            {
                if (mResults[6] == MarkType.Free) return 6;
            }
            if (mResults[6].ToString() == computer && (mResults[6] & mResults[8]) == mResults[6])
            {
                if (mResults[7] == MarkType.Free) return 7;
            }

            if (mResults[9].ToString() == computer && (mResults[9] & mResults[10]) == mResults[9])
            {
                if (mResults[11] == MarkType.Free) return 11;
            }
            if (mResults[10].ToString() == computer && (mResults[10] & mResults[11]) == mResults[10])
            {
                if (mResults[9] == MarkType.Free) return 9;
            }
            if (mResults[9].ToString() == computer && (mResults[9] & mResults[11]) == mResults[9])
            {
                if (mResults[10] == MarkType.Free) return 10;
            }

            if (mResults[12].ToString() == computer && (mResults[12] & mResults[13]) == mResults[12])
            {
                if (mResults[14] == MarkType.Free) return 14;
            }
            if (mResults[13].ToString() == computer && (mResults[13] & mResults[14]) == mResults[13])
            {
                if (mResults[12] == MarkType.Free) return 12;
            }
            if (mResults[12].ToString() == computer && (mResults[12] & mResults[14]) == mResults[12])
            {
                if (mResults[13] == MarkType.Free) return 13;
            }

            #endregion

            #region VertiCheck

            if (mResults[6].ToString() == computer && (mResults[6] & mResults[9]) == mResults[6])
            {
                if (mResults[12] == MarkType.Free) return 12;
            }
            if (mResults[9].ToString() == computer && (mResults[9] & mResults[12]) == mResults[9])
            {
                if (mResults[6] == MarkType.Free) return 6;
            }
            if (mResults[6].ToString() == computer && (mResults[6] & mResults[12]) == mResults[6])
            {
                if (mResults[9] == MarkType.Free) return 9;
            }

            if (mResults[7].ToString() == computer && (mResults[7] & mResults[10]) == mResults[7])
            {
                if (mResults[13] == MarkType.Free) return 13;
            }
            if (mResults[10].ToString() == computer && (mResults[10] & mResults[13]) == mResults[10])
            {
                if (mResults[7] == MarkType.Free) return 7;
            }
            if (mResults[7].ToString() == computer && (mResults[7] & mResults[13]) == mResults[7])
            {
                if (mResults[10] == MarkType.Free) return 10;
            }

            if (mResults[8].ToString() == computer && (mResults[8] & mResults[11]) == mResults[8])
            {
                if (mResults[14] == MarkType.Free) return 14;
            }
            if (mResults[11].ToString() == computer && (mResults[11] & mResults[14]) == mResults[11])
            {
                if (mResults[8] == MarkType.Free) return 8;
            }
            if (mResults[8].ToString() == computer && (mResults[8] & mResults[14]) == mResults[8])
            {
                if (mResults[11] == MarkType.Free) return 11;
            }
            #endregion

            #region DiagCheck

            if (mResults[6].ToString() == computer && (mResults[6] & mResults[10]) == mResults[6])
            {
                if (mResults[14] == MarkType.Free) return 14;
            }
            if (mResults[10].ToString() == computer && (mResults[10] & mResults[14]) == mResults[10])
            {
                if (mResults[6] == MarkType.Free) return 6;
            }
            if (mResults[6].ToString() == computer && (mResults[6] & mResults[14]) == mResults[6])
            {
                if (mResults[10] == MarkType.Free) return 10;
            }
            
            if (mResults[8].ToString() == computer && (mResults[8] & mResults[10]) == mResults[8])
            {
                if (mResults[12] == MarkType.Free) return 12;
            }
            if (mResults[10].ToString() == computer && (mResults[10] & mResults[12]) == mResults[10])
            {
                if (mResults[8] == MarkType.Free) return 8;
            }
            if (mResults[8].ToString() == computer && (mResults[8] & mResults[12]) == mResults[8])
            {
                if (mResults[10] == MarkType.Free) return 10;
            }

            #endregion

            //if no wins then return 0
            return 0;
        }

        private int CheckForLoss(int x)
        {
            //check for cases that are resulting in a loss if none return the variable that was sent in
            if (moveCounter == 1)
            {
                if (mResults[10].ToString() == person)
                    return 7;
            }
            if(moveCounter == 3)
            {
                if(mResults[6] == MarkType.Cross && (mResults[13] & mResults[6]) == mResults[6])
                {
                    return 12;
                }
                if (mResults[6] == MarkType.Cross && (mResults[11] & mResults[6]) == mResults[6])
                {
                    return 8;
                }
            }
            if(moveCounter == 3)
            {
                if(mResults[14] == MarkType.Cross &&(mResults[14] & mResults[7]) == mResults[14])
                {
                    return 11;
                }
                else if(mResults[14] == MarkType.Cross && (mResults[14] & mResults[9]) == mResults[14])
                {
                    return 13;
                }
            }

            #region HoriCheck
            //check hori for losses or wins
            if (mResults[6 ] == MarkType.Cross && (mResults[6 ] & mResults[7 ]) == mResults[6])
            {
                if (mResults[8] == MarkType.Free) return 8;
            }

            if (mResults[7 ] == MarkType.Cross && (mResults[7 ] & mResults[8 ]) == mResults[7 ])
            {
                if (mResults[6] == MarkType.Free) return 6;
            }
            if (mResults[6 ] == MarkType.Cross && (mResults[6 ] & mResults[8 ]) == mResults[6 ])
            {
                if (mResults[7] == MarkType.Free) return 7;
            }

            if (mResults[9 ] == MarkType.Cross && (mResults[9 ] & mResults[10]) == mResults[9 ])
            {
                if (mResults[11] == MarkType.Free) return 11;
            }
            if (mResults[10] == MarkType.Cross && (mResults[10] & mResults[11]) == mResults[10])
            {
                if (mResults[9] == MarkType.Free) return 9;
            }
            if (mResults[9 ] == MarkType.Cross && (mResults[9 ] & mResults[11]) == mResults[9 ])
            {
                if (mResults[10] == MarkType.Free) return 10;
            }

            if (mResults[12] == MarkType.Cross && (mResults[12] & mResults[13]) == mResults[12])
            {
                if (mResults[14] == MarkType.Free) return 14;
            }
            if (mResults[13] == MarkType.Cross && (mResults[13] & mResults[14]) == mResults[13])
            {
                if (mResults[12] == MarkType.Free) return 12;
            }
            if (mResults[12] == MarkType.Cross && (mResults[12] & mResults[14]) == mResults[12])
            {
                if (mResults[13] == MarkType.Free) return 13;
            }

            if (mResults[6 ] == MarkType.Nought && (mResults[6 ] & mResults[7 ]) == mResults[6 ])
            {
                if (mResults[8] == MarkType.Free) return 8;
            }
            if (mResults[7 ] == MarkType.Nought && (mResults[7 ] & mResults[8 ]) == mResults[7 ])
            {
                if (mResults[6] == MarkType.Free) return 6;
            }
            if (mResults[6 ] == MarkType.Nought && (mResults[6 ] & mResults[8 ]) == mResults[6 ])
            {
                if (mResults[7] == MarkType.Free) return 7;
            }

            if (mResults[9 ] == MarkType.Nought && (mResults[9 ] & mResults[10]) == mResults[9 ])
            {
                if (mResults[11] == MarkType.Free) return 11;
            }
            if (mResults[10] == MarkType.Nought && (mResults[10] & mResults[11]) == mResults[10])
            {
                if (mResults[9] == MarkType.Free) return 9;
            };
            if (mResults[9 ] == MarkType.Nought && (mResults[9 ] & mResults[11]) == mResults[9 ])
            {
                if (mResults[10] == MarkType.Free) return 10;
            }

            if (mResults[12] == MarkType.Nought && (mResults[12] & mResults[13]) == mResults[12])
            {
                if (mResults[14] == MarkType.Free) return 14;
            }
            if (mResults[13] == MarkType.Nought && (mResults[13] & mResults[14]) == mResults[13])
            {
                if (mResults[12] == MarkType.Free) return 12;
            }
            if (mResults[12] == MarkType.Nought && (mResults[12] & mResults[14]) == mResults[12])
            {
                if (mResults[13] == MarkType.Free) return 13;
            }
            #endregion

            #region VertiCheck   
            if (mResults[6 ] == MarkType.Cross && (mResults[6 ] & mResults[9 ]) == mResults[6 ])
            {
                if (mResults[12] == MarkType.Free) return 12;
            }
            if (mResults[9 ] == MarkType.Cross && (mResults[9 ] & mResults[12]) == mResults[9 ])
            {
                if (mResults[6] == MarkType.Free) return 6;
            }
            if (mResults[6 ] == MarkType.Cross && (mResults[6 ] & mResults[12]) == mResults[6 ])
            {
                if (mResults[9] == MarkType.Free) return 9;
            }

            if (mResults[7 ] == MarkType.Cross && (mResults[7 ] & mResults[10]) == mResults[7 ])
            {
                if (mResults[13] == MarkType.Free) return 13;
            }
            if (mResults[10] == MarkType.Cross && (mResults[10] & mResults[13]) == mResults[10])
            {
                if (mResults[7] == MarkType.Free) return 7;
            }
            if (mResults[7 ] == MarkType.Cross && (mResults[7 ] & mResults[13]) == mResults[7 ])
            {
                if (mResults[10] == MarkType.Free) return 10;
            }

            if (mResults[8 ] == MarkType.Cross && (mResults[8 ] & mResults[11]) == mResults[8 ])
            {
                if (mResults[14] == MarkType.Free) return 14;
            }
            if (mResults[11] == MarkType.Cross && (mResults[11] & mResults[14]) == mResults[11])
            {
                if (mResults[8] == MarkType.Free) return 8;
            }
            if (mResults[8 ] == MarkType.Cross && (mResults[8 ] & mResults[14]) == mResults[8 ])
            {
                if (mResults[11] == MarkType.Free) return 11;
            }

            if (mResults[6 ] == MarkType.Nought && (mResults[6 ] & mResults[9 ]) == mResults[6 ])
            {
                if (mResults[12] == MarkType.Free) return 12;
            }
            if (mResults[9 ] == MarkType.Nought && (mResults[9 ] & mResults[12]) == mResults[9 ])
            {
                if (mResults[6] == MarkType.Free) return 6;
            }
            if (mResults[6 ] == MarkType.Nought && (mResults[6 ] & mResults[12]) == mResults[6 ])
            {
                if (mResults[9] == MarkType.Free) return 9;
            }

            if (mResults[7 ] == MarkType.Nought && (mResults[7 ] & mResults[10]) == mResults[7 ])
            {
                if (mResults[13] == MarkType.Free) return 13;
            }
            if (mResults[10] == MarkType.Nought && (mResults[10] & mResults[13]) == mResults[10])
            {
                if (mResults[7] == MarkType.Free) return 7;
            }
            if (mResults[7 ] == MarkType.Nought && (mResults[7 ] & mResults[13]) == mResults[7 ])
            {
                if (mResults[10] == MarkType.Free) return 10;
            }

            if (mResults[8 ] == MarkType.Nought && (mResults[8 ] & mResults[11]) == mResults[8 ])
            {
                if (mResults[14] == MarkType.Free) return 14;
            }
            if (mResults[11] == MarkType.Nought && (mResults[11] & mResults[14]) == mResults[11])
            {
                if (mResults[8] == MarkType.Free) return 8;
            }
            if (mResults[8 ] == MarkType.Nought && (mResults[8 ] & mResults[14]) == mResults[8 ])
            {
                if (mResults[11] == MarkType.Free) return 11;
            }
            #endregion

            #region DiagCheck   
            if (mResults[6 ] == MarkType.Cross && (mResults[6 ] & mResults[10]) == mResults[6 ])
            {
                if (mResults[14] == MarkType.Free) return 14;
            }
            if (mResults[10] == MarkType.Cross && (mResults[10] & mResults[14]) == mResults[10])
            {
                if (mResults[6] == MarkType.Free) return 6;
            }
            if (mResults[6 ] == MarkType.Cross && (mResults[6 ] & mResults[14]) == mResults[6 ])
            {
                if (mResults[10] == MarkType.Free) return 10;
            }

            if (mResults[8 ] == MarkType.Cross && (mResults[8 ] & mResults[10]) == mResults[8 ])
            {
                if (mResults[12] == MarkType.Free) return 12;
            }
            if (mResults[10] == MarkType.Cross && (mResults[10] & mResults[12]) == mResults[10])
            {
                if (mResults[8] == MarkType.Free) return 8;
            }
            if (mResults[8 ] == MarkType.Cross && (mResults[8 ] & mResults[12]) == mResults[8 ])
            {
                if (mResults[10] == MarkType.Free) return 10;
            }

            if (mResults[6 ] == MarkType.Nought && (mResults[6 ] & mResults[10]) == mResults[6 ])
            {
                if (mResults[14] == MarkType.Free) return 14;
            }
            if (mResults[10] == MarkType.Nought && (mResults[10] & mResults[14]) == mResults[10])
            {
                if (mResults[6] == MarkType.Free) return 6;
            }
            if (mResults[6 ] == MarkType.Nought && (mResults[6 ] & mResults[14]) == mResults[6 ])
            {
                if (mResults[10] == MarkType.Free) return 10;
            }

            if (mResults[8 ] == MarkType.Nought && (mResults[8 ] & mResults[10]) == mResults[8])
            {
                if (mResults[12] == MarkType.Free) return 12;
            }
            if (mResults[10] == MarkType.Nought && (mResults[10] & mResults[12]) == mResults[10])
            {
                if (mResults[8] == MarkType.Free) return 8;
            }
            if (mResults[8 ] == MarkType.Nought && (mResults[8 ] & mResults[12]) == mResults[8])
            {
                if (mResults[10] == MarkType.Free) return 10;
            }
            #endregion
            //traps
            #region Traps

            if (mResults[6] == MarkType.Cross && (mResults[6] & mResults[14]) == mResults[6])
            {
                if (mResults[10] == MarkType.Nought)
                {
                    if (mResults[7] == MarkType.Free && (mResults[7] & mResults[8] & mResults[9] & mResults[11] & mResults[12]) == mResults[7])
                    {
                        return 7;
                    }
                }
            }

            #endregion

            //check if corners are taken or if center is taken
            if (mResults[6] == MarkType.Free)
            {
                if(moveCounter == 0)
                {
                    return 6;
                }
            }
            else if(mResults[10] == MarkType.Free)
            {
                return 10;
            }

            if(moveCounter == 7)
            {
                if(mResults[10] == MarkType.Nought && (mResults[10] & mResults[12] & mResults[13]) == mResults[10])
                {
                    if (mResults[7] == MarkType.Cross && (mResults[7] & mResults[8] & mResults[9] & mResults[14]) == mResults[7])
                    {
                        return 11;
                    }

                }
            }

            

            return x;
        }

        private void AIMakesMove(int random)
        {
            //an int will be sent in and placed at the spot in the array then run through the switch statement so that 
            //a board position will be taken where that int is
            //depending on if the computer is x or o then it will place the correct value
            mResults[random] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;
            bool turn = mPlayer1Turn;

            if (turn)
            {
                Output.Content = "Player O's Turn"; Output.Background = Brushes.Yellow;

            }
            else
            {
                Output.Content = "Player X's Turn"; Output.Background = Brushes.Yellow;

            }

            switch (random)
            {
                case 6:
                    var but = (Button)Button0_0;
                    but.Content = mPlayer1Turn ? "X" : "O";
                    if (!mPlayer1Turn)
                        but.Foreground = Brushes.Blue;
                    break;
                case 7:
                    var but1 = (Button)Button1_0;
                    but1.Content = mPlayer1Turn ? "X" : "O";
                    if (!mPlayer1Turn)
                        but1.Foreground = Brushes.Blue;
                    break;
                case 8:
                    var but2 = (Button)Button2_0;
                    but2.Content = mPlayer1Turn ? "X" : "O";
                    if (!mPlayer1Turn)
                        but2.Foreground = Brushes.Blue;
                    break;
                case 9:
                    var but3 = (Button)Button0_1;
                    but3.Content = mPlayer1Turn ? "X" : "O";
                    if (!mPlayer1Turn)
                        but3.Foreground = Brushes.Blue;
                    break;
                case 10:
                    var but4 = (Button)Button1_1;
                    but4.Content = mPlayer1Turn ? "X" : "O";
                    if (!mPlayer1Turn)
                        but4.Foreground = Brushes.Blue;
                    break;
                case 11:
                    var but5 = (Button)Button2_1;
                    but5.Content = mPlayer1Turn ? "X" : "O";
                    if (!mPlayer1Turn)
                        but5.Foreground = Brushes.Blue;
                    break;
                case 12:
                    var but6 = (Button)Button0_2;
                    but6.Content = mPlayer1Turn ? "X" : "O";
                    if (!mPlayer1Turn)
                        but6.Foreground = Brushes.Blue;
                    break;
                case 13:
                    var but7 = (Button)Button1_2;
                    but7.Content = mPlayer1Turn ? "X" : "O";
                    if (!mPlayer1Turn)
                        but7.Foreground = Brushes.Blue;
                    break;
                case 14:
                    var but8 = (Button)Button2_2;
                    but8.Content = mPlayer1Turn ? "X" : "O";
                    if (!mPlayer1Turn)
                        but8.Foreground = Brushes.Blue;
                    break;
            }
        }

        private void CheckForWinner()
        {
            //this will check for winners for both the AI and the player
            //if there is a winner then the board will be changed to reflect who is the winner
            //mGameEnded bool will be switched so that the game and AI knows the game is over and no more moves can be made
            #region horizontal wins
            //check hori wins
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                mGameEnded = true;
                Output.Background = Brushes.Aqua;

                //highlight winning
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;

                if (!mPlayer1Turn)
                {
                    Output.Content = "X is the Winner!";


                }
                else
                {
                    Output.Content = "O is the Winner!";

                }

                return;

            }

            if (mResults[9] != MarkType.Free && (mResults[9] & mResults[10] & mResults[11]) == mResults[9])
            {
                mGameEnded = true;
                Output.Background = Brushes.Aqua;

                //highlight winning
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;

                if (!mPlayer1Turn)
                {
                    Output.Content = "X is the Winner!";
                }
                else
                {
                    Output.Content = "O is the Winner!";

                }
                return;

            }

            if (mResults[12] != MarkType.Free && (mResults[12] & mResults[13] & mResults[14]) == mResults[12])
            {
                mGameEnded = true;
                Output.Background = Brushes.Aqua;

                //highlight winning
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;

                if (!mPlayer1Turn)
                {
                    Output.Content = "X is the Winner!";
                }
                else
                {
                    Output.Content = "O is the Winner!";

                }
                return;

            }
            #endregion

            #region vertical wins
            //vertical win col 0
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[9] & mResults[12]) == mResults[6])
            {
                mGameEnded = true;
                Output.Background = Brushes.Aqua;

                //highlight winning
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;

                if (!mPlayer1Turn)
                {
                    Output.Content = "X is the Winner!";
                }
                else
                {
                    Output.Content = "O is the Winner!";

                }
                return;

            }
            //vertical win col 1
            if (mResults[7] != MarkType.Free && (mResults[7] & mResults[10] & mResults[13]) == mResults[7])
            {
                mGameEnded = true;
                Output.Background = Brushes.Aqua;

                //highlight winning
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;

                if (!mPlayer1Turn)
                {
                    Output.Content = "X is the Winner!";
                }
                else
                {
                    Output.Content = "O is the Winner!";

                }
                return;

            }
            //vertical win col 2
            if (mResults[8] != MarkType.Free && (mResults[8] & mResults[11] & mResults[14]) == mResults[8])
            {
                mGameEnded = true;
                Output.Background = Brushes.Aqua;

                //highlight winning
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;

                if (!mPlayer1Turn)
                {
                    Output.Content = "X is the Winner!";
                }
                else
                {
                    Output.Content = "O is the Winner!";

                }
                return;

            }
            #endregion

            #region diagnol wins
            //diag 1
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[10] & mResults[14]) == mResults[6])
            {
                mGameEnded = true;
                Output.Background = Brushes.Aqua;

                //highlight winning
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;

                if (!mPlayer1Turn)
                {
                    Output.Content = "X is the Winner!";
                }
                else
                {
                    Output.Content = "O is the Winner!";

                }
                return;

            }
            //diag 2
            if (mResults[8] != MarkType.Free && (mResults[8] & mResults[10] & mResults[12]) == mResults[8])
            {
                mGameEnded = true;
                Output.Background = Brushes.Aqua;

                //highlight winning
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;

                if (!mPlayer1Turn)
                {
                    Output.Content = "X is the Winner!";
                }
                else
                {
                    Output.Content = "O is the Winner!";

                }
                return;
            }

            #endregion

            #region Tie

            //check if board filled
            if (moveCounter == 9)
            {
                mGameEnded = true;
                Output.Background = Brushes.Gray;

                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {

                    Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Orange;
                    Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Orange;
                    Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Orange;

                });
                Output.Content = "There is no winner";
            }
            #endregion

        }

        private void Replay(object sender, RoutedEventArgs e)
        {
            //for the replay button, simply calls the newgame method
            //I could make the replay button call the newgame method directly however if I wanted to do something else 
            //when the replay button is pressed as well as start a new game then I will just have to add the code here
            //instead of implenting if statements in the newgame method
            NewGame();
        }


    }
}
