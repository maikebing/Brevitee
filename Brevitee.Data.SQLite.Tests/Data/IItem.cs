using System;
namespace SampleData
{
    public interface IItem
    {
        string ASIN { get; }
        string Description { get; }
        string DetailPage { get; }
        long? Id { get; set; }
        string LargeImageURL { get; }
        string MediumImageURL { get; }
        string Name { get; set; }
        string SmallImageURL { get; }
        string SubTitle { get; }

        string Genre { get; }
        string ESRBRating { get; }
        string ReleaseDate { get; }
    }
}
