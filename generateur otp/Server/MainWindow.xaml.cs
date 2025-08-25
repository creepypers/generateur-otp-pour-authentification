using System;
using System.Numerics;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Media;

namespace Server
{
    public partial class MainWindow : Window
    {
        // La Seed de notre algorithme un nombre aleatoire  
        private const long Seed = 738649254;

        // Nombre d'or
        const double goldenRatio = 1.6180339887498948482;

        // Variable qui contiendra le OTP generee
        private string currentOTP;

        // Variable pour afficher le OTP precedent
        private string previousOTP;

        // Nombre premier
        readonly BigInteger prime = 18446744073709551557;

        // Nombre de tentative
        private int attempts = 5;
        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            InitializeTimer();
            UpdateOTP();
            txtPrevious.Text = "OTP précédent: Aucun";
        }

        private void InitializeTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (s, e) => UpdateOTP();
            timer.Start();
        }
        private void UpdateOTP()
        {
            var timeWindow = DateTimeOffset.UtcNow.ToUnixTimeSeconds() / 60;
            string newOTP = GenerateOTP(timeWindow);

            if (newOTP != currentOTP)
            {
                previousOTP = currentOTP;
                currentOTP = newOTP;
                txtPrevious.Text = $"OTP précédent: {previousOTP ?? "Aucun"}";
            }
        }
        //Exactement le meme algorithme que le client
        private string GenerateOTP(long timeWindow)
        {


            
            // On va faire une operation arithmetique sur le resultat du calcul avec le nombre d'or
            double phiComponent = Math.Pow(goldenRatio, (double)(Seed % 100 + timeWindow % 100));
            long transformedSeed = (long)(Seed * phiComponent);
            
            // On va faire une encore plus d'operation arithmetique sur le resultat du calcul avec le nombre d'or
            BigInteger combined = new BigInteger(transformedSeed ^ timeWindow);
            combined = (combined % prime) * (combined + prime);
            

            // On vas rendre le OTP plus aléatoire et chaotique
            for(int i = 0; i < 5; i++)
            {
                combined = combined * (combined % 1000000007) + (combined >> 32);
                combined = combined ^ (combined << 16);
            }
            
            // On va extraire 8 chiffres de l'OTP
            string otpString = combined.ToString().PadLeft(20, '0');
            int startIndex = (int)(combined % 12);
            return otpString.Substring(startIndex, 8);
        }
        //Verification de l'OTP avec le OTP genere par le client
        private void BtnValidate_Click(object sender, RoutedEventArgs e)
        {
            //Si l'OTP est correct, on affiche un message de confirmation
            if (txtInput.Text == currentOTP)
            {
                txtResult.Text = "✅ Accès confirmé !";
                txtResult.Foreground = new SolidColorBrush(Color.FromRgb(39, 174, 96));
                attempts = 5;
            }
            //Si l'OTP est incorrect, on affiche un message de refus et on decremente le nombre de tentative
            else
            {
                //Si le nombre de tentative est egal a 0, on ferme l'application
                if (--attempts <= 0)
                {
                    txtResult.Text = "❌ Trop de tentatives - Fermeture !";
                    //On change la couleur du texte en rouge
                    txtResult.Foreground = new SolidColorBrush(Color.FromRgb(192, 57, 43));
                    //On cree un timer qui va fermer l'application apres 1 seconde
                    var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
                    //On ferme l'application apres 1 seconde
                    timer.Tick += (s, args) =>
                    {
                        timer.Stop();
                        Application.Current.Shutdown();
                    };
                    timer.Start();
                }
                else
                {
                    //On affiche le nombre de tentative restante
                    txtResult.Text = $"❌ Accès refusé ! {attempts} tentative{(attempts > 1 ? "s" : "")} restante{(attempts > 1 ? "s" : "")}";
                    //On change la couleur du texte en rouge
                    txtResult.Foreground = new SolidColorBrush(Color.FromRgb(231, 76, 60));
                }
            }
            //On vide le champ de saisie
            txtInput.Clear();
        }
    }
}