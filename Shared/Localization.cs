/* Copyright (c) 2024 Rick (rick 'at' gibbed 'dot' us)
 *
 * This software is provided 'as-is', without any express or implied
 * warranty. In no event will the authors be held liable for any damages
 * arising from the use of this software.
 *
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 *
 * 1. The origin of this software must not be misrepresented; you must not
 *    claim that you wrote the original software. If you use this software
 *    in a product, an acknowledgment in the product documentation would
 *    be appreciated but is not required.
 *
 * 2. Altered source versions must be plainly marked as such, and must not
 *    be misrepresented as being the original software.
 *
 * 3. This notice may not be removed or altered from any source
 *    distribution.
 */

// This file is shared between SAM.Game and SAM.Picker.
// Modified for Simplified Chinese support.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace SAM.Localization
{
    /// <summary>
    /// Runtime localization support for SAM.
    /// English is the source language and every English string doubles as the translation key.
    /// </summary>
    public static class L10n
    {
        public const string EnglishLanguage = "en";
        public const string SimplifiedChineseLanguage = "zh-CN";

        public const string SteamSimplifiedChinese = "schinese";
        public const string SteamTraditionalChinese = "tchinese";
        public const string SteamEnglish = "english";

        private const string SettingsFileName = "SAM.language";

        private static readonly Dictionary<string, string> SimplifiedChinese = new()
        {
            // Window titles.
            { "Steam Achievement Manager 7.0", "Steam 成就管理器 7.0" },
            { "Steam Achievement Manager 7.0 | Pick a game... Any game...", "Steam 成就管理器 7.0 | 选个游戏……任意游戏……" },

            // Language menu.
            { "Language", "语言" },

            // Manager tool bar.
            { "Commit Changes", "提交更改" },
            { "Store achievements and statistics for active game.", "保存当前游戏的成就和统计数据。" },
            { "Refresh", "刷新" },
            { "Refresh achievements and statistics for active game.", "刷新当前游戏的成就和统计数据。" },
            { "Reset", "重置" },
            { "Reset achievements and/or statistics for active game.", "重置当前游戏的成就和/或统计数据。" },
            { "Download status", "下载状态" },

            // Manager tabs.
            { "Achievements", "成就" },
            { "Statistics", "统计" },

            // Manager achievement list columns.
            { "Name", "名称" },
            { "Description", "描述" },
            { "Unlock Time", "解锁时间" },

            // Manager statistics list columns.
            { "Value", "数值" },
            { "Extra", "附加" },

            // Manager achievements tool bar.
            { "Lock All", "全部锁定" },
            { "Lock all achievements.", "锁定所有成就。" },
            { "Invert All", "全部反转" },
            { "Invert all achievements.", "反转所有成就的解锁状态。" },
            { "Unlock All", "全部解锁" },
            { "Unlock all achievements.", "解锁所有成就。" },
            { "Show only", "仅显示" },
            { "locked", "已锁定" },
            { "unlocked", "已解锁" },
            { "Filter", "筛选" },
            { "Type at least 3 characters that must appear in the name or description", "输入至少 3 个字符，名称或描述中需包含这些字符" },

            // Manager statistics options.
            { "I understand by modifying the values of stats, I may screw things up and can't blame anyone but myself.", "我明白修改统计数据可能会造成问题，责任由我自己承担。" },

            // Stat flags (the "Extra" column).
            { "None", "无" },
            { "IncrementOnly", "仅可增加" },
            { "Protected", "受保护" },
            { "UnknownPermission", "未知权限" },

            // Picker tool bar.
            { "Refresh Games", "刷新游戏列表" },
            { "Add Game", "添加游戏" },
            { "Game filtering", "游戏筛选" },
            { "Show &games", "显示游戏(&G)" },
            { "Show &demos", "显示演示(&D)" },
            { "Show &mods", "显示模组(&M)" },
            { "Show &junk", "显示其他(&J)" },
            { "App {0}", "应用 {0}" },

            // Picker status.
            { "Downloading game list...", "正在下载游戏列表……" },
            { "Checking game ownership...", "正在检查游戏拥有情况……" },
            { "Displaying {0} games. Total {1} games.", "正在显示 {0} 个游戏，共 {1} 个游戏。" },
            { "Downloading {0} game icons...", "正在下载 {0} 个游戏图标……" },

            // Manager status.
            { "Downloading {0} icons...", "正在下载 {0} 个图标……" },
            { "Retrieving stat information...", "正在获取统计信息……" },
            { "Retrieved {0} achievements and {1} statistics.", "已获取 {0} 个成就和 {1} 项统计。" },
            { "Failed to load schema.", "加载统计架构失败。" },
            { "Error while retrieving stats: {0}", "获取统计数据时出错：{0}" },
            { "generic error -- this usually means you don't own the game", "一般性错误——通常表示你没有拥有此游戏" },
            { "Error when handling achievements retrieval.", "处理成就获取时出错。" },
            { "Error when handling achievements retrieval:\n{0}", "处理成就获取时出错：\n{0}" },
            { "Error when handling stats retrieval.", "处理统计数据获取时出错。" },
            { "Error when handling stats retrieval:\n{0}", "处理统计数据获取时出错：\n{0}" },

            // Manager storing.
            { "An error occurred while setting the state for {0}, aborting store.", "设置成就“{0}”的解锁状态时出错，已中止保存。" },
            { "An error occurred while setting the value for {0}, aborting store.", "设置统计“{0}”的数值时出错，已中止保存。" },
            { "An error occurred while storing, aborting.", "保存时出错，已中止。" },
            { "Stored {0} achievements and {1} statistics.", "已保存 {0} 个成就和 {1} 项统计。" },
            { "Stat is protected! -- you can't modify it", "该统计数据受保护！无法修改。" },
            { "Invalid value", "数值无效" },
            { "Sorry, but this is a protected achievement and cannot be managed with Steam Achievement Manager.", "抱歉，此成就受保护，无法通过 Steam 成就管理器管理。" },

            // Manager resetting.
            { "Are you absolutely sure you want to reset stats?", "你确定要重置统计数据吗？" },
            { "Do you want to reset achievements too?", "是否同时重置成就？" },
            { "Really really sure?", "真的确定吗？" },

            // MessageBox titles.
            { "Error", "错误" },
            { "Warning", "警告" },
            { "Question", "询问" },
            { "Information", "信息" },

            // Generic failures.
            { "Failed.", "操作失败。" },

            // Startup messages.
            { "Could not parse application ID from command line argument.", "无法从命令行参数解析应用程序 ID。" },
            { "This tool declines to being run from the Steam directory.", "本工具不能从 Steam 目录中运行。" },
            { "Steam is not running. Please start Steam then run this tool again.", "Steam 未运行。请先启动 Steam，然后重新运行本工具。" },
            { "Steam is not running. Please start Steam then run this tool again.\n\n({0})", "Steam 未运行。请先启动 Steam，然后重新运行本工具。\n\n({0})" },
            {
                "Steam is not running. Please start Steam then run this tool again.\n\n" +
                "If you have the game through Family Share, the game may be locked due to\n" +
                "the Family Share account actively playing a game.\n\n({0})",
                "Steam 未运行。请先启动 Steam，然后重新运行本工具。\n\n" +
                "如果你是通过家庭共享获得该游戏的，由于共享账号正在玩游戏，该游戏可能已被锁定。\n\n({0})"
            },
            { "You've caused an exceptional error!", "发生了一个异常错误！" },
        };

        // Remembers the original English text of every control/column/item so the UI can be
        // re-localized in both directions without touching the designer files.
        private static readonly Dictionary<object, string> Originals = new();

        private static string _Language = EnglishLanguage;
        private static bool _Initialized;

        public static string Language => _Language;

        public static bool IsChinese => _Language == SimplifiedChineseLanguage;

        /// <summary>
        /// Loads the configured language, or picks a default from the OS UI culture.
        /// Safe to call more than once.
        /// </summary>
        public static void Initialize()
        {
            if (_Initialized == true)
            {
                return;
            }

            _Initialized = true;

            var configured = LoadConfiguredLanguage();
            if (configured != null)
            {
                _Language = configured;
                return;
            }

            _Language = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "zh"
                ? SimplifiedChineseLanguage
                : EnglishLanguage;
        }

        public static void SetLanguage(string language)
        {
            var normalized = NormalizeLanguage(language) ?? EnglishLanguage;
            if (_Language == normalized)
            {
                return;
            }

            _Language = normalized;
            SaveConfiguredLanguage();
        }

        /// <summary>
        /// Translates a single string. Unknown strings are returned unchanged.
        /// </summary>
        public static string T(string text)
        {
            if (text == null || text.Length == 0)
            {
                return text;
            }

            if (_Language != SimplifiedChineseLanguage)
            {
                return text;
            }

            return SimplifiedChinese.TryGetValue(text, out var translated) == true
                ? translated
                : text;
        }

        /// <summary>
        /// Translates a format string, then formats it with the current culture.
        /// </summary>
        public static string T(string format, params object[] args)
        {
            if (format == null)
            {
                return null;
            }

            return string.Format(CultureInfo.CurrentCulture, T(format), args);
        }

        /// <summary>
        /// Returns the Steam language name to use for schema and asset lookups.
        /// </summary>
        public static string ResolveSteamLanguage(string steamLanguage)
        {
            if (IsChinese == true)
            {
                return SteamSimplifiedChinese;
            }

            if (string.IsNullOrEmpty(steamLanguage) == true)
            {
                return SteamEnglish;
            }

            // The Steam client language follows the user's Steam setting, which may still be
            // Chinese even when the tool itself is displayed in English. Fall back to English
            // in that case so the window and its contents always agree.
            if (steamLanguage == SteamSimplifiedChinese || steamLanguage == SteamTraditionalChinese)
            {
                return SteamEnglish;
            }

            return steamLanguage;
        }

        /// <summary>
        /// Gets the best available display name for an app: localized Chinese name when
        /// Chinese is active, otherwise the name as provided by the Steam client.
        /// </summary>
        public static string GetAppName(SAM.API.Client client, uint appId)
        {
            if (client == null)
            {
                return null;
            }

            string name = null;

            if (IsChinese == true)
            {
                name = GetAppData(client, appId, "name_localized/" + SteamSimplifiedChinese);
                if (string.IsNullOrEmpty(name) == true)
                {
                    name = GetAppData(client, appId, "name_localized/" + SteamTraditionalChinese);
                }
            }

            if (string.IsNullOrEmpty(name) == true)
            {
                name = GetAppData(client, appId, "name");
            }

            return string.IsNullOrEmpty(name) == true ? null : name;
        }

        /// <summary>
        /// Re-localizes the given control and every child control (and its tool strip items),
        /// including column headers of list and grid views.
        /// </summary>
        public static void Apply(Control control)
        {
            if (control == null)
            {
                return;
            }

            control.Text = Translate(control, control.Text);

            if (control is ListView listView)
            {
                foreach (ColumnHeader column in listView.Columns)
                {
                    column.Text = Translate(column, column.Text);
                }
            }
            else if (control is DataGridView grid)
            {
                foreach (DataGridViewColumn column in grid.Columns)
                {
                    column.HeaderText = Translate(column, column.HeaderText);
                }
            }

            if (control is ToolStrip toolStrip)
            {
                foreach (ToolStripItem item in toolStrip.Items)
                {
                    ApplyItem(item);
                }
            }

            foreach (Control child in control.Controls)
            {
                Apply(child);
            }
        }

        /// <summary>
        /// Creates the "Language" drop down button used by both windows.
        /// </summary>
        public static ToolStripDropDownButton CreateLanguageButton(Action onChanged)
        {
            var chineseItem = new ToolStripMenuItem("简体中文");
            var englishItem = new ToolStripMenuItem("English");

            var button = new ToolStripDropDownButton();
            button.Text = "Language";
            button.ToolTipText = "Language";
            button.DisplayStyle = ToolStripItemDisplayStyle.Text;
            button.DropDownItems.Add(chineseItem);
            button.DropDownItems.Add(englishItem);

            chineseItem.Click += (sender, e) => ChangeLanguage(SimplifiedChineseLanguage, onChanged);
            englishItem.Click += (sender, e) => ChangeLanguage(EnglishLanguage, onChanged);

            button.DropDownOpening += (sender, e) =>
            {
                chineseItem.Checked = IsChinese;
                englishItem.Checked = IsChinese == false;
            };

            return button;
        }

        private static void ChangeLanguage(string language, Action onChanged)
        {
            SetLanguage(language);

            if (onChanged != null)
            {
                onChanged();
            }
        }

        private static void ApplyItem(ToolStripItem item)
        {
            item.Text = Translate(item, item.Text);
            item.ToolTipText = Translate(item, item.ToolTipText);

            if (item is ToolStripDropDownItem dropDown)
            {
                foreach (ToolStripItem child in dropDown.DropDownItems)
                {
                    ApplyItem(child);
                }
            }
        }

        private static string Translate(object owner, string current)
        {
            if (Originals.TryGetValue(owner, out var original) == false)
            {
                original = current ?? string.Empty;
                Originals.Add(owner, original);
            }

            return T(original);
        }

        private static string GetAppData(SAM.API.Client client, uint appId, string key)
        {
            try
            {
                return client.SteamApps001.GetAppData(appId, key);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static string NormalizeLanguage(string language)
        {
            if (string.IsNullOrEmpty(language) == true)
            {
                return null;
            }

            switch (language.Trim().ToLowerInvariant())
            {
                case "zh":
                case "zh-cn":
                case "zh-hans":
                case "chinese":
                case "schinese":
                case "simplified chinese":
                {
                    return SimplifiedChineseLanguage;
                }

                case "en":
                case "en-us":
                case "english":
                {
                    return EnglishLanguage;
                }

                default:
                {
                    return null;
                }
            }
        }

        private static string GetSettingsPath()
        {
            return Path.Combine(Application.StartupPath, SettingsFileName);
        }

        private static string LoadConfiguredLanguage()
        {
            try
            {
                var path = GetSettingsPath();
                if (File.Exists(path) == false)
                {
                    return null;
                }

                return NormalizeLanguage(File.ReadAllText(path));
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static void SaveConfiguredLanguage()
        {
            try
            {
                File.WriteAllText(GetSettingsPath(), _Language);
            }
            catch (Exception)
            {
                // The tool may be run from a read only location; the language choice is then session only.
            }
        }
    }
}
