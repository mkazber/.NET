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

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StringBuilder cheat;
        private int roundCounter;
        private int player1WinCounter;
        private int player2WinCounter;
        private int playerTurn;

        private UIElementCollection buttons;

        #region Private Members

        /// <summary>
        /// Holds the current results of cells in the active game
        /// </summary>
        private MarkType[] mResults;

        /// <summary>
        /// True if it is player 1's turn (X) or player 2's turn (O)
        /// </summary>
        private bool mPlayer1Turn;

        /// <summary>
        /// True if the game has ended
        /// </summary>
        private bool mGameEnded;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
            cheat = new StringBuilder();
            buttons = Container.Children;
        }

        #endregion

        /// <summary>
        /// Starts a new game and clears all values back to the start
        /// </summary>
        private void NewGame()
        {
            // Create a new blank array of free cells
            mResults = new MarkType[9];

            for (var i = 0; i < mResults.Length; i++)
                mResults[i] = MarkType.Free;

            // Make sure Player 1 starts the game
            mPlayer1Turn = WhichPlayerTurn();

            // Interate every button on the grid...
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                // Change background, foreground and content to default values
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            // Make sure the game hasn't finished
            mGameEnded = false;
            playerTurn = mPlayer1Turn ? 1 : 2;
            Title = "Round " + ++roundCounter + " Player1 |" + player1WinCounter + " : " + player2WinCounter + "| Player2" + "|||Player" + playerTurn;
        }

        /// <summary>
        /// Handles a button click event
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">The events of the click</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Start a new game on the click after it finished
            if (mGameEnded)
            {
                NewGame();
                return;
            }

            // Cast the sender to a button
            var button = (Button)sender;

            // Find the buttons position in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            // Don't do anything if the cell already has a value in it
            if (mResults[index] != MarkType.Free)
                return;

            // Set the cell value based on which players turn it is
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;

            // Set button text to the result
            button.Content = mPlayer1Turn ? "X" : "O";

            // Change noughts to green
            if (!mPlayer1Turn)
                button.Foreground = Brushes.Red;
            else
                button.Foreground = Brushes.Blue;
            // Toggle the players turns
            mPlayer1Turn ^= true;
            playerTurn = mPlayer1Turn ? 1 : 2;
            Title = "Round " + roundCounter + " Player1 |" + player1WinCounter + " : " + player2WinCounter + "| Player2" + "|||Player" + playerTurn;
            // Check for a winner
            CheckForWinner();
        }

        /// <summary>
        /// Checks if there is a winner of a 3 line straight
        /// </summary>
        private void CheckForWinner()
        {
            #region Horizontal Wins

            // Check for horizontal wins
            //
            //  - Row 0
            //
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                // Game ends
                WhoWon(mResults[0]);
                mGameEnded = true;

                // Highlight winning cells in green
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
                return;
            }
            //
            //  - Row 1
            //
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                // Game ends
                WhoWon(mResults[3]);
                mGameEnded = true;

                // Highlight winning cells in green
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
                return;
            }
            //
            //  - Row 2
            //
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                // Game ends
                WhoWon(mResults[6]);
                mGameEnded = true;

                // Highlight winning cells in green
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
                return;
            }

            #endregion

            #region Vertical Wins

            // Check for vertical wins
            //
            //  - Column 0
            //
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                // Game ends
                WhoWon(mResults[0]);
                mGameEnded = true;

                // Highlight winning cells in green
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
                return;
            }
            //
            //  - Column 1
            //
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                // Game ends
                WhoWon(mResults[1]);
                mGameEnded = true;

                // Highlight winning cells in green
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
                return;
            }
            //
            //  - Column 2
            //
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                // Game ends
                WhoWon(mResults[2]);
                mGameEnded = true;

                // Highlight winning cells in green
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
                return;
            }

            #endregion

            #region Diagonal Wins

            // Check for diagonal wins
            //
            //  - Top Left Bottom Right
            //
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {

                // Game ends
                WhoWon(mResults[0]);
                mGameEnded = true;

                // Highlight winning cells in green
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
                return;
            }
            //
            //  - Top Right Bottom Left
            //
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                // Game ends
                WhoWon(mResults[2]);
                mGameEnded = true;

                // Highlight winning cells in green
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
                return;
            }

            #endregion

            #region No Winners

            // Check for no winner and full board
            if (!mResults.Any(f => f == MarkType.Free))
            {
                // Game ended
                mGameEnded = true;

                // Turn all cells orange
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });

                // MessageBox.Show("Remis", "Remis", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }

            #endregion
        }

        private void MainWindow_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (mGameEnded) return;
            if (e.Key == Key.F6)
            {
                cheat.Length = 0;
                return;
            }
            cheat.Append(e.Key.ToString());
            if (cheat.Equals(Cheats.WinGame))
            {
                mGameEnded = true;
                if (mPlayer1Turn)
                {
                    MessageBox.Show("Player1 Won!", "Hurra!", MessageBoxButton.OK, MessageBoxImage.Hand);
                    cheat.Length = 0;
                    player1WinCounter++;
                }
                else
                {
                    MessageBox.Show("Player2 Won!", "Hurra!", MessageBoxButton.OK, MessageBoxImage.Hand);
                    player2WinCounter++;
                    cheat.Length = 0;
                }
                
            }
            else if (cheat.Equals(Cheats.DeleteOneEnemyMove))
            {
                RemoveOneMove(mPlayer1Turn);
                cheat.Length = 0;
            }
        }

        private void RemoveOneMove(bool playerTurn)
        {
            if (playerTurn)
            {
                for (int i = 0; i < mResults.Length; i++)
                {
                    if (mResults[i] == MarkType.Nought)
                    {
                        Button button = buttons[i] as Button;
                        mResults[i] = MarkType.Free;
                        button.Content = "";
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < mResults.Length; i++)
                {
                    if (mResults[i] == MarkType.Cross)
                    {
                        Button button = buttons[i] as Button;
                        mResults[i] = MarkType.Free;
                        button.Content = "";
                        // mPlayer1Turn = true;
                        break;
                    }
                }
            }
        }

        private void WhoWon(MarkType type)
        {
            if (type == MarkType.Cross)
            {
                player1WinCounter++;
                MessageBox.Show("Player1 Won!!!", "Winner!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else if (type == MarkType.Nought)
            {
                player2WinCounter++;
                MessageBox.Show("Player2 Won!!!", "Winner!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        private bool WhichPlayerTurn()
        {
            return new Random().Next(0, 100) >= 50;
        }
    }
}
