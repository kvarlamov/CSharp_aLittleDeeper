// See https://aka.ms/new-console-template for more information

using System.Threading.Channels;
using p13_Channels;

var unboundedChannel = Channel.CreateUnbounded<Envelope>();

var writer = new Producer(unboundedChannel.Writer);
var reader = new Consumer(unboundedChannel.Reader);

var t1 = Task.Factory.StartNew(async () => await writer.Write());
var t2 = Task.Factory.StartNew(async () => await reader.Read());

await Task.WhenAll(t1, t2);
Console.WriteLine("MESSAGE ENDED");
