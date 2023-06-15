namespace s3893749_s3912792_a1.interfaces;

/* The IDatabaseObject interface provides a means to pass an IDatabaseObject to a method and let the method decide what
 * to do with it -> Dependency Injection
 */
public interface IDatabaseObject
{
    /* The classes that implement the IDatabaseObject must implement the property 'CustomerId'. */
    public int CustomerId { get; set; }
}