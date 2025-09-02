namespace Accounting.Application.Exceptions;

public class EntityNotFoundException : NotFoundException
{
    public EntityNotFoundException(string entityName, object id)
        : base($"{entityName} with id {id} not found")
    {
    }

    public static EntityNotFoundException For<T>(object id)
    {
        //Optional
        //Use with DisplayAttribute (custom name) in the entity class
        //Example:
        /*
         * [Display(Name = "Account Detail")]
         * public class Account
         * {
         *     public Guid Id { get; set; }
         *      ...
         * }
         *
         */

        //var entityType = typeof(T);
        //var displayAttr = entityType.GetCustomAttribute<DisplayAttribute>();
        //var entityName = displayAttr?.Name ?? entityType.Name;

        return new EntityNotFoundException(typeof(T).Name, id);
    }
}