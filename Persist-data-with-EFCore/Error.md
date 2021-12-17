# 使用DBinitializer为数据库Seed Data后，使用HttpRepl测试Web API没有问题，但本地数据库依旧为空，似乎context.SaveChanges方法未被执行

解决方法：

```c#
// Program.cs
builder.Services.AddDbContext<PizzaContext>(options =>
    options.UseSqlite("Data Source=ContosoPizza.db"));
```

连接位于根目录的本地数据库ContosoPizza.db后，

```c#
// DbInitializer.cs
	context.SaveChanges();
```

Seed操作完成后执行此方法，将会再bin/Debug/net6.0目录下重新创建一个ContosoPizza.db，该数据库内有记录，而根目录的数据库并未被更新。

将其复制并替换至根目录即可。

# 使用post方法提交json格式数据时，提示500 Internal Server Error

> System.InvalidCastException: The field of type System.Int32 must be a string, array or ICollection type.

解决方法：

MaxLenght attribute is for a string , not for a type of int, so just delete it.

```c#
	[Display(Name = "Active: ")]
    [MaxLength(1)] // this will fail
    public int      Active { get; set; }

    [Display(Name = "GUID: ")] 
    [MaxLength(37)] //this is okay
    public string   GUID { get; set; }
```

