/// Puzzlang is a pattern matching language for abstract games and puzzles. See http://www.polyomino.com/puzzlang.
///
/// Copyright © Polyomino Games 2018. All rights reserved.
/// 
/// This is free software. You are free to use it, modify it and/or 
/// distribute it as set out in the licence at http://www.polyomino.com/licence.
/// You should have received a copy of the licence with the software.
/// 
/// This software is distributed in the hope that it will be useful, but with
/// absolutely no warranty, express or implied. See the licence for details.
/// 
using System;
using System.Collections.Generic;
using System.Linq;
using NsfxrLib;
using DOLE;
using System.IO;

namespace Nsfxr {
  class Program {

    const string Version = "Nsfxr 1.0b";
    static string _help = "Nsfxr <seed> [<output> /verbose]";
    static TextWriter _out = Console.Out;
    static bool _xdebug = false;

    static readonly Dictionary<string, Action<string>> _options
      = new Dictionary<string, Action<string>>(StringComparer.CurrentCultureIgnoreCase) {
        { "verbose",   (a) => { Logger.Level = 1; } },
        { "xdebug",   (a) => { _xdebug = true; } },
      };

    static readonly Dictionary<int, Func<RNG, Patch>> _seedlookup = new Dictionary<int, Func<RNG, Patch>> {
      { 0, r=>Global.PickupCoin(r) },
      { 1, r=>Global.LaserShoot(r) },
      { 2, r=>Global.Explosion(r) },
      { 3, r=>Global.PowerUp(r) },
      { 4, r=>Global.HitHurt(r) },
      { 5, r=>Global.Jump(r) },
      { 6, r=>Global.BlipSelect(r) },
      { 7, r=>Global.PushSound(r) },
      { 8, r=>Global.Random(r) },
      { 9, r=>Global.Bird(r) },
    };

    static void Main(string[] args) {
      _out.WriteLine(Version);
      var options = OptionParser.Create(_options, _help);
      if (!options.Parse(args))
        return;
      if (_xdebug) {
        options.Parse(new string[] { "/verbose", "36772507" });  // crate move
        //options.Parse(new string[] { "/verbose", "36772507", "test.wav" });  // crate move
      }
      if (options.PathsCount < 1)
        _out.WriteLine("Must specify seed");
      else {
        var nsound = options.GetPath(0).SafeIntParse() ?? 0;
        var filename = options.GetPath(1) ?? options.GetPath(0);
        if (!Path.HasExtension(filename)) filename = Path.ChangeExtension(filename, ".wav");
        if (!_seedlookup.ContainsKey(nsound % 100))
          _out.WriteLine("Invalid seed");
        else DoGenerate(nsound, filename);
      }
    }

    static void DoGenerate(int nseed, string pathname) {
      _out.WriteLine("Writing sound '{0}' to {1}", nseed, pathname);
      var rng = new RNG(nseed / 100);
      var patch = _seedlookup[nseed % 100](rng);
      Logger.WriteLine(1, "*** seed is {0}\n{1}", nseed, patch);
      var gen = new Generator(patch);
      var samps = gen.Generate();
      Logger.WriteLine(1, "Generated {0} samples.", samps.Count);
      Global.SaveWAV(samps, pathname);
    }
  }
}
