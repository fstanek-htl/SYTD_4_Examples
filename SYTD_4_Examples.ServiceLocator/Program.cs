using SYTD_4_Examples.ServiceLocator.Complex;
using SYTD_4_Examples.ServiceLocator.Complex.Implementations;
using SYTD_4_Examples.ServiceLocator.Complex.Interfaces;

var builder = new ComplexServiceLocator();
builder.Add<ILeafService, MyLeafService>();
builder.Add<IDependentService, MyDependentService>();

var service = builder.GetService<IDependentService>();
Console.WriteLine(service.GetText());
Console.ReadLine();