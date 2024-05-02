namespace SuperClock.Models.UI
{
    public class SnowFlakeModel : Image
    {

        #region Properties

        public bool IsAnimating { get; set; }
        public double StartingTranslationX { get; set; }

        #endregion


        #region Methods

        public bool IsTimeForGo()
        {
            var random = new Random();

            var number = random.Next(0,100);

            return
                number < 3;
        }
        public async void Animate()
        {
            var random = new Random();
            var timeSpeed = random.Next(5500, 9000);

            await 
                this.TranslateTo(this.TranslationX, 1200, (uint)timeSpeed);

            this.IsAnimating = false;
            this.TranslationY = -50;
        }

        #endregion
    }
}
