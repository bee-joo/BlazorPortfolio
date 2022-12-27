namespace BlazorPortfolio.Client
{
    public class SemesterList : List<int>
    {
        public SemesterList() 
        {
            for (int i = 3; i <= 7; i++)
            {
                Add(i);
            }
        }
    }
}
