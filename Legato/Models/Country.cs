namespace Legato.Models
{
    /// <summary>
    ///     POCO for country
    /// </summary>
    public class Country
    {
        /// <summary>
        ///     Constructor for Country class
        /// </summary>
        /// <param name="name">Name of the country</param>
        public Country(string name)
        {
            Name = name;
        }

        /// <summary>
        ///     Name property of the country
        /// </summary>
        public string Name { get; }
    }
}