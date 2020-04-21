## Modular EF
An attempt at creating a way to ensure that we can re use our domain modules while at the same time ensuring Ef Core functionalities such as transaction, relation, and migrations, to name a few, work normally.

## Detail
This solution contains two projects:

1. Inventory [Class Libarry]
1. ModularEf [Asp.Net Web App]

The inventory module is added to act as a proof of concept that we can modularize our domain with Ef.

The web app uses Asp.Net Core Identity for migration, just to ensure that the approach works with existing technologies.

## Approach

Each of the modules that wants to use Ef Core has to have a class in the following format.

```csharp
public static class EntityRegisterer
{
    public static void UseInventory(this ModelBuilder builder)
    {
        builder.Entity<Item>().ToTable("tbl_item");
    }
}
```

The name of the class and the method can be anything. Pick a name that makes sense.

This class adds an extension method to the ModelBuilder class.

So, any entity registration and entity configuration we need to do for this particular module, we can do here. 

In the example above, we are adding an entity `Item` and then mapping it to Table `tbl_item`.

In our actual context class, which usually resides in its own project or in the web app (Recommended for this approach), we simply call the extensiton method in the OnModelCreating method to use configurations from a certain module.

```csharp
public class BaseContext : IdentityDbContext<IdentityUser>
{
    public BaseContext(DbContextOptions<BaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.UseInventory();
    }
}
```

`modelBuilder.UseInventory()` adds the inventory module configuration. This will be picked up by our application as well as the tools such as Ef Core Migrations.

If we want to add Account, we simply call the extension method of Account module.

```diff
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);
    modelBuilder.UseInventory();
+   modelBuilder.UseAccount();
}
```

If two of your modules use the same extension method name, we have a couple of ways that can be resolved.

1. Rename one of the method name
    
    This is the simplest and straight forward approach. We simply rename one of the methods so that there is not name clashing. However, this approach might not be a viable option if the module is used by multiple other projects.

1. Call the extension method directly
    
    Since extension methods are simply static methods, we can call them directly by providing the model builder instance.

    ```diff
    modelBuilder.UseReport(); // Inventory Report
    - modelBuilder.UseReport(); // Account Report
    + Account.EntityRegisterer.UseReport(modelBuilder);
    ```
    
    This avoids the name clashing.