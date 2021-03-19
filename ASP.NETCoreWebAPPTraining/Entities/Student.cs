namespace ASP.NETCoreWebAPPTraining.Entities
{
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string StudentNo { get; set; }

        public int Age { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public bool IsDeleted { get; set; }
    }
}