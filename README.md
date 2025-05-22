# ModelUI

**ModelUI** is a cross-platform desktop application boilerplate built using the [Avalonia](https://avaloniaui.net/) framework in C#. It aims to provide a solid starting point for building clean, modern desktop apps with ease.

This project integrates the **free version** of the [Actipro Avalonia Controls](https://github.com/Actipro/Avalonia-Controls) to deliver elegant UI components and interactions.

> ⚠️ The original Actipro demo contains a lot of sample code. This repo distills it into a minimal working base for real-world applications.

---

## ✨ Features

* ✅ Side Navigation Bar
* ✅ Smooth Transitions Between Views
* ✅ Designed for Non-MVVM Architecture
* ✅ Day & Night Theme Support
* ✅ Cross-Platform (Windows & Linux – even Raspberry Pi!)
* ✅ Lightweight Release Output
* ✅ Built-in Configuration File Read/Write
* ✅ Boilerplate Database Module for Info Storage

---

## 🚧 TODO

* [ ] Refactor and improve code structure
* [ ] Fix view transition overlap (Sidebar currently overlaps second view)
* [ ] Accepting contributions! Feel free to fork and send a PR

---

## 🖥️ Supported Platforms

* ✅ **Windows** (10 and above)
* ✅ **Linux** (Tested on Ubuntu, Linux Mint, Raspberry Pi OS, etc.)

You can build and publish the app as a **self-contained executable** using Avalonia. This includes the framework and works well on both platforms. Be sure to enable the "Produce single file" option in your release configuration.

---

## 🛠️ Getting Started

### 📦 Fork & Rename the App

If you want to create your own app based on this boilerplate:

1. **Fork** the repository.
2. Run the `RenameProject.bat` script (Windows only).
3. Enter your new project name when prompted.

This will update solution files and namespaces automatically.

> 🐧 A Linux-compatible `.sh` version is planned for future updates.

---

### 💾 Backup Utility

The included `BackupData.bat` script can help you archive your project:

* Edit the following line to specify a backup path:

  ```bat
  set BackupLocation="E:\Backups\"
  ```
* When run, it creates a `.7z` archive with a timestamp.

> Requires [7-Zip](https://www.7-zip.org/).
> Currently works on **Windows only**.

---

## 📝 Notes

* I previously used [SukiUI](https://github.com/kikipoulet/SukiUI), which is a great UI toolkit. However, I found Actipro's Avalonia controls more stable and elegant for my needs, especially with recent breaking changes in SukiUI (No offense to the dev — great work there!).

---

## 📸 Screenshots

> Replace local paths with actual GitHub-relative paths or URLs for images to display correctly.

**ModelUI in Action:**

![ModelUI](Docs/ModelUI.gif)

**Release Configuration Example:**

![Release Settings](Docs/Screenshot%20(52).png)

---

## 🤝 Contributing

Pull requests and contributions are welcome! If you find a bug or have a feature request, feel free to open an issue.

---

## 📄 License

This project is open-source and available under the [MIT License](LICENSE).

---
