namespace LibPoint.Domain.Models.Reviews;

public class ReviewModel
{
    public Guid Id { get; set; }
    public int Rating { get; set; }           // 1-10 arasında puan
    public string Comment { get; set; }
    public string FullName { get; set; }
    public DateTime? CreatedTime { get; set; }


    public Guid AppuUserId { get; set; }
    // public AppUserModel AppUser { get; set; }  

    public Guid BookId { get; set; }
    // public BookModel Book { get; set; }       
}