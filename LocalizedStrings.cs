namespace MissingEventFlagsCheckerPlugin
{
    public static class LocalizedStrings
    {
        readonly static Dictionary<string, string> s_localizedStrings = [];
        readonly static string s_langCode = "en";

        public static void Initialize(string langCode)
        {
            if (s_localizedStrings.Count == 0 || s_langCode != langCode)
            {
                s_localizedStrings.Clear();

                var res = ReadLangResFile(langCode);

                using (System.IO.StringReader reader = new System.IO.StringReader(res))
                {
                    string? s = reader.ReadLine();

                    while (s is not null)
                    {
                        if (!string.IsNullOrWhiteSpace(s))
                        {
                            var entry = s.Split('=');
                            s_localizedStrings[entry[0]] = entry[1];
                        }

                        s = reader.ReadLine();
                    }

                    if (s is null)
                    {
                        return;
                    }
                }
            }
        }

        public static string Find(string key, string current)
        {
            return s_localizedStrings.TryGetValue(key, out string? value) ? value : current;
        }

        public static void LocalizeForm(Control form)
        {
            form.SuspendLayout();
            string formName = form.GetType().Name;

            form.Text = Find($"{formName}.TitleName", form.Text);

            var listOfControls = AssembleLocalizableControlsList(form);

            foreach (var c in listOfControls)
            {

                if (c is DataGridView dgv)
                {
                    var columns = dgv.Columns;
                    foreach (DataGridViewColumn dgv_c in columns)
                    {
                        var current = dgv_c.HeaderText;
                        var updated = Find($"{formName}.{dgv_c.Name}", current);
                        if (!ReferenceEquals(current, updated))
                        {
                            dgv_c.HeaderText = updated;
                        }
                    }
                }

                else if (c is Control r)
                {
                    var current = r.Text;
                    if (!string.IsNullOrEmpty(current))
                    {
                        var updated = Find($"{formName}.{r.Name}", current);
                        if (!ReferenceEquals(current, updated))
                        {
                            r.Text = updated;
                        }
                    }
                }
            }
            form.ResumeLayout();
        }

        static List<Control> AssembleLocalizableControlsList(Control formRoot)
        {
            List<Control> controlsList = [];

            foreach (var c in formRoot.Controls)
            {
                if (c is ListControl or TextBoxBase or LinkLabel or NumericUpDown or ContainerControl or VScrollBar or HScrollBar)
                    continue;

                else if (c is Control r)
                {
                    if (r.HasChildren)
                    {
                        controlsList.AddRange(AssembleLocalizableControlsList(r));
                    }
                    controlsList.Add(r);
                }
            }

            return controlsList;
        }

        static string ReadLangResFile(string langCode)
        {
            string? contentTxt = null;

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            string resName = $"lang_{langCode}.txt";

            // Try outside file first
            var offResPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(assembly.Location)!, resName);
            if (!System.IO.File.Exists(offResPath))
            {
                try
                {
                    resName = assembly.GetManifestResourceNames().Single(str => str.EndsWith(resName));
                }
                catch (InvalidOperationException)
                {
                    // Sanity Fallback
                    return ReadLangResFile("en");
                }

                using (var stream = assembly.GetManifestResourceStream(resName))
                {
                    using (var reader = new System.IO.StreamReader(stream!))
                    {
                        contentTxt = reader.ReadToEnd();
                    }
                }
            }
            else
            {
                contentTxt = System.IO.File.ReadAllText(offResPath);
            }

            return contentTxt;
        }
    }
}
