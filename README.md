# 🚀 **FastProjects.Caching**

![Build Status](https://github.com/Fast-Projects-NET/FastProjects.Caching/actions/workflows/test.yml/badge.svg)
![NuGet](https://img.shields.io/nuget/v/FastProjects.Caching.svg)
![NuGet Downloads](https://img.shields.io/nuget/dt/FastProjects.Caching.svg)
![License](https://img.shields.io/github/license/Fast-Projects-NET/FastProjects.Caching.svg)
![Last Commit](https://img.shields.io/github/last-commit/Fast-Projects-NET/FastProjects.Caching.svg)
![GitHub Stars](https://img.shields.io/github/stars/Fast-Projects-NET/FastProjects.Caching.svg)
![GitHub Forks](https://img.shields.io/github/forks/Fast-Projects-NET/FastProjects.Caching.svg)

> 🚨 ALERT: Project Under Development
> This project is not yet production-ready and is still under active development. Currently, it's being used primarily for personal development needs. However, contributions are more than welcome! If you'd like to collaborate, feel free to submit issues or pull requests. Your input can help shape the future of FastProjects!

---

## 📚 **Overview**

Collection of interfaces to work with cache (for example, [Redis](https://redis.io/) integration).

---

## 🛠 **Roadmap**

- ✅ [ICacheService](src/FastProjects.Caching/ICacheService.cs) - Interface for cache service
- ✅ [RedisCacheService](src/FastProjects.Caching/RedisCacheService.cs) - Implementation of `ICacheService` that uses Redis
- ✅ [InMemoryCacheService](src/FastProjects.Caching/InMemoryCacheService.cs) - Implementation of `ICacheService` that uses in-memory cache
- ✅ [CacheOptions](src/FastProjects.Caching/CacheOptions.cs) - Options for cache service (expiration time, etc.)
- ✅ [ICachedQuery](src/FastProjects.Caching/ICachedQuery.cs) - Interface for cached [query](https://github.com/Fast-Projects-NET/FastProjects.SharedKernel/blob/main/src/FastProjects.SharedKernel/IQuery.cs)
- ✅ [QueryCachingBehavior](src/FastProjects.Caching/QueryCachingBehavior.cs) - pipeline behavior for MediatR that caches ICachedQuery

---

## 🚀 **Installation**

You can download the NuGet package using the following command to install:
```bash
dotnet add package FastProjects.Caching
```

---

## 🤝 **Contributing**

This project is still under development, but contributions are welcome! Whether you’re opening issues, submitting pull requests, or suggesting new features, we appreciate your involvement. For more details, please check the [contribution guide](CONTRIBUTING.md). Let’s build something amazing together! 🎉

---

## 📄 **License**

FastProjects is licensed under the **MIT License**. See the [LICENSE](LICENSE) file for full details.
