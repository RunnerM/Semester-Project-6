// See https://aka.ms/new-console-template for more information

using Data.Domain;
using EFCore.Utils;

var ctx = new Context();
var k = ctx.Set<Movie>().Single(x => x.Id == new Guid());
Console.WriteLine("Hello, World!");