﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;

namespace ctstone.Redis.RedisCommands
{
    class RedisHash : RedisCommand<Dictionary<string, string>>
    {
        public RedisHash(string command, params object[] args)
            : base(ParseStream, command, args)
        { }

        private static Dictionary<string, string> ParseStream(Stream stream)
        {
            string[] fieldValues = RedisReader.ReadMultiBulkUTF8(stream);
            return HashMapper.GetDict(fieldValues);
        }
    }

    class RedisHash<T> : RedisCommand<T>
        where T : new()
    {
        public RedisHash(string command, params object[] args)
            : base(ParseStream, command, args)
        { }

        private static T ParseStream(Stream stream)
        {
            string[] fieldValues = RedisReader.ReadMultiBulkUTF8(stream);
            return HashMapper.ReflectHash<T>(fieldValues);
        }
    }
}