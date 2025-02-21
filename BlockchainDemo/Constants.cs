﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoChain.Models
{
    public class Constants
    {
        public static class PUBSUB_CHANNEL
        {
            public static string TEST { get { return "TEST"; } }
            public static string BLOCKCHAIN { get { return "BLOCKCHAIN"; } }
            public static string TRANSACTION { get { return "TRANSACTION"; } }
        }

        public static string PUBSUB_CHANNEL_PPREFIX = Guid.NewGuid().ToString();

        #region Redis
        public static string REDIS_SERVER { get { return Environment.GetEnvironmentVariable("REDIS_SERVER") ?? "127.0.0.1"; } }//  throw new KeyNotFoundException("Environment variable `REDIS_SERVER` not found."); } }
        public static string APP_NANE { get { return Environment.GetEnvironmentVariable("APP_NANE") ?? "CryptoChain"; } } //throw new KeyNotFoundException("Environment variable `APP_NANE` not found."); } }

        public static int REDIS_PORT
        {
            get
            {
                if (int.TryParse(Environment.GetEnvironmentVariable("REDIS_PORT") ?? "6379", out var _REDIS_PORT))
                    return _REDIS_PORT;
                throw new KeyNotFoundException("Environment variable `REDIS_PORT` is invalid.");
            }
        }

        public static string REDIS_PASSWORD { get { return Environment.GetEnvironmentVariable("REDIS_PASSWORD") ?? string.Empty; } }

        public static int REDIS_TIMEOUT_IN_MILLISEC
        {
            get
            {
                if (int.TryParse(Environment.GetEnvironmentVariable("REDIS_TIMEOUT_IN_MILLISEC") ?? "60000", out var _REDIS_TIMEOUT_IN_MILLISEC))
                    return _REDIS_TIMEOUT_IN_MILLISEC;
                throw new KeyNotFoundException("Environment variable `REDIS_TIMEOUT_IN_MILLISEC` is invalid.");
            }
        }

        public static int REDIS_CONNECT_RETRY_LIMIT
        {
            get
            {
                if (int.TryParse(Environment.GetEnvironmentVariable("REDIS_CONNECT_RETRY_LIMIT") ?? "25", out var _REDIS_CONNECT_RETRY_LIMIT))
                    return _REDIS_CONNECT_RETRY_LIMIT;
                throw new KeyNotFoundException("Environment variable `REDIS_CONNECT_RETRY_LIMIT` is invalid.");
            }
        }

        public static bool REDIS_ABORT_ON_CONNECT_FAIL { get { return Environment.GetEnvironmentVariable("REDIS_ABORT_ON_CONNECT_FAIL")?.Trim().ToLower() == "true"; } }

        #endregion



        public static int HTTP_PORT
        {
            get
            {
                if (int.TryParse(Environment.GetEnvironmentVariable("HTTP_PORT") ?? "3000", out var _HTTP_PORT))
                    return _HTTP_PORT;
                throw new KeyNotFoundException("Environment variable `HTTP_PORT` is invalid.");
            }
        }
        public static string ROOT_NODE_URL
        {
            get
            {
                return Environment.GetEnvironmentVariable("ROOT_NODE_URL") ?? $"http://localhost:{HTTP_PORT}";
            }
        }
        public static int MINING_RATE_IN_MILLISEC
        {
            get
            {
                if (int.TryParse(Environment.GetEnvironmentVariable("MINING_RATE_IN_MILLISEC") ?? "60000", out var _MINING_RATE_IN_MILLISEC))
                    return _MINING_RATE_IN_MILLISEC;
                throw new KeyNotFoundException("Environment variable `MINING_RATE_IN_MILLISEC` is invalid.");
            }
        }



        public static DateTime JS_TIMESTAMP { get { return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc); } }
        public static DateTime GENESIS_TIMESTAMP { get { return JS_TIMESTAMP; } }
        public static string GENESIS_PREV_HASH { get { return string.Empty; } }
        public static long GENESIS_NONCE { get { return 0L; } }
        public static int GENESIS_DIFFICULTY { get { return 3; } }

        public static long STARTING_BALANCE { get { return 1000; } }

        public static string MINING_REWARD_INPUT { get { return "mining-reward"; } }
        public static string MINING_REWARD_INPUT_SIGNATURE { get { return string.Empty; } }
        public static long MINING_REWARD { get { return 50; } }

    }
}
