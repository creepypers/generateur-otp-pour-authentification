using System;
using System.Numerics;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Media;

namespace Client
{
    public partial class MainWindow : Window
    {
        // La Seed de notre algorithme un nombre aleatoire base sur la constante de Boltzmann la meme que le serveur
        private const long Seed = 738649254;

        private DispatcherTimer timer;

        // La derniere date de mise a jour de l'OTP
        private DateTimeOffset lastUpdateTime;

        // La constante de nombre d'or
        const double goldenRatio = 1.6180339887498948482;

        readonly BigInteger prime = 18446744073709551557; // Grand nombre premier utilise pour faire des operations arithmetiques sur le seed et le nombre d'or il est assez connu

        // La derniere OTP generee
        public MainWindow()
        {
            InitializeComponent();
            InitializeTimer();
            UpdateDisplay();
        }

        // Initialisation du timer
        private void InitializeTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (s, e) => UpdateDisplay();
            timer.Start();
        }

        // Mise a jour de l'affichage de l'OTP
        private void UpdateDisplay()
        {
            var timeWindow = DateTimeOffset.UtcNow.ToUnixTimeSeconds() / 60;
            string newOTP = GenerateOTP(timeWindow);
            
            txtOTP.Text = newOTP;
            
            var now = DateTimeOffset.UtcNow;
            var secondsRemaining = 60 - now.Second;
            txtTimer.Text = $"🕒 Actualisation dans {secondsRemaining:D2} secondes";
        }

        // Generation de l'OTP
        private string GenerateOTP(long timeWindow)
        {
            
            
            // Étape 1: Combinaison non linéaire avec le nombre d'or
            double phiComponent = Math.Pow(goldenRatio, (double)(Seed % 100 + timeWindow % 100));
            long transformedSeed = (long)(Seed * phiComponent);
            
            // Étape 2: Transformation cryptographique
            BigInteger combined = new BigInteger(transformedSeed ^ timeWindow);
            // Pour notre encore plus complexifier la generation du OTP nous allons faire une operation arithmetique sur le resultat du calcul avec le nombre d'or
            combined = (combined % prime) * (combined + prime);
            
            // Étape 3: Mélange chaotique pour rendre le OTP plus aléatoire
            for(int i = 0; i < 5; i++)
            {
                combined = combined * (combined % 1000000007) + (combined >> 32);
                combined = combined ^ (combined << 16);
            }
            
            // Étape 4: Normalisation
            string otpString = combined.ToString().PadLeft(20, '0'); // 20 chiffres
            
            // Étape 5: Extraction dynamique
            int startIndex = (int)(combined % 12); // 20 - 8 = 12 positions possibles
            // On va extraire 8 chiffres de l'OTP
            return otpString.Substring(startIndex, 8);
        }

        // Copie de l'OTP
        private void BtnCopy_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtOTP.Text))
            {
                Clipboard.SetText(txtOTP.Text);
                
                // On cree un timer qui va copier l'OTP dans le clipboard apres 500ms
                var timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(500) };
                timer.Tick += (s, args) =>
                {
                    timer.Stop();
                };
                timer.Start();
            }
        }
    }
}