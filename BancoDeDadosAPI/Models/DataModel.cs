namespace BancoDeDadosAPI.Models
{
    public class DataModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Number { get; set; }
        public float Decimal { get; set; }
        public DateTime Date { get; set; }

        public DataModel() { }

        public DataModel(int id, string txt, int num, float dec, DateTime date)
        {
            Id = id;
            Text = txt;
            Number = num;
            Decimal = dec;
            Date = date;
        }
    }
}