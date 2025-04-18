using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Security;

namespace MelissaData {
	public class mdName : IDisposable {
		private IntPtr i;

		public enum ProgramStatus {
			NoError = 0,
			ConfigFile = 1,
			LicenseExpired = 2,
			DatabaseExpired = 3,
			Unknown = 4
		}
		public enum NameHints {
			DefinitelyFull = 1,
			VeryLikelyFull = 2,
			ProbablyFull = 3,
			Varying = 4,
			ProbablyInverse = 5,
			VeryLikelyInverse = 6,
			DefinitelyInverse = 7,
			MixedFirstName = 8,
			MixedLastName = 9
		}
		public enum Population {
			Male = 1,
			Mixed = 2,
			Female = 3
		}
		public enum Aggression {
			Aggressive = 1,
			Neutral = 2,
			Conservative = 3
		}
		public enum Salutations {
			Formal = 0,
			Informal = 1,
			FirstLast = 2,
			Slug = 3,
			Blank = 4
		}
		public enum MiddleNameLogic {
			ParseLogic = 0,
			HyphenatedLast = 1,
			MiddleName = 2
		}
		public enum ResultCdDescOpt {
			ResultCodeDescriptionLong = 0,
			ResultCodeDescriptionShort = 1
		}

		[SuppressUnmanagedCodeSecurity]
		private class mdNameUnmanaged {
			[DllImport("mdName", EntryPoint = "mdNameCreate", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdNameCreate();
			[DllImport("mdName", EntryPoint = "mdNameDestroy", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdNameDestroy(IntPtr i);
			[DllImport("mdName", EntryPoint = "mdNameSetPathToNameFiles", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdNameSetPathToNameFiles(IntPtr i, IntPtr p1);
			[DllImport("mdName", EntryPoint = "mdNameInitializeDataFiles", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdNameInitializeDataFiles(IntPtr i);
			[DllImport("mdName", EntryPoint = "mdNameGetInitializeErrorString", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdNameGetInitializeErrorString(IntPtr i);
			[DllImport("mdName", EntryPoint = "mdNameSetLicenseString", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdNameSetLicenseString(IntPtr i, IntPtr p1);
			[DllImport("mdName", EntryPoint = "mdNameGetBuildNumber", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdNameGetBuildNumber(IntPtr i);
			[DllImport("mdName", EntryPoint = "mdNameGetDatabaseDate", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdNameGetDatabaseDate(IntPtr i);
			[DllImport("mdName", EntryPoint = "mdNameGetDatabaseExpirationDate", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdNameGetDatabaseExpirationDate(IntPtr i);
			[DllImport("mdName", EntryPoint = "mdNameGetLicenseExpirationDate", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdNameGetLicenseExpirationDate(IntPtr i);
			[DllImport("mdName", EntryPoint = "mdNameSetPrimaryNameHint", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdNameSetPrimaryNameHint(IntPtr i, Int32 p1);
			[DllImport("mdName", EntryPoint = "mdNameSetSecondaryNameHint", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdNameSetSecondaryNameHint(IntPtr i, Int32 p1);
			[DllImport("mdName", EntryPoint = "mdNameSetFirstNameSpellingCorrection", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdNameSetFirstNameSpellingCorrection(IntPtr i, Int32 p1);
			[DllImport("mdName", EntryPoint = "mdNameSetMiddleNameLogic", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdNameSetMiddleNameLogic(IntPtr i, Int32 p1);
			[DllImport("mdName", EntryPoint = "mdNameSetGenderPopulation", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdNameSetGenderPopulation(IntPtr i, Int32 p1);
			[DllImport("mdName", EntryPoint = "mdNameSetGenderAggression", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdNameSetGenderAggression(IntPtr i, Int32 p1);
			[DllImport("mdName", EntryPoint = "mdNameAddSalutation", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdNameAddSalutation(IntPtr i, Int32 p1);
			[DllImport("mdName", EntryPoint = "mdNameSetSalutationPrefix", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdNameSetSalutationPrefix(IntPtr i, IntPtr p1);
			[DllImport("mdName", EntryPoint = "mdNameSetSalutationSuffix", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdNameSetSalutationSuffix(IntPtr i, IntPtr p1);
			[DllImport("mdName", EntryPoint = "mdNameSetSalutationSlug", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdNameSetSalutationSlug(IntPtr i, IntPtr p1);
			[DllImport("mdName", EntryPoint = "mdNameClearProperties", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdNameClearProperties(IntPtr i);
			[DllImport("mdName", EntryPoint = "mdNameSetFullName", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdNameSetFullName(IntPtr i, IntPtr p1);
			[DllImport("mdName", EntryPoint = "mdNameSetPrefix", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdNameSetPrefix(IntPtr i, IntPtr p1);
			[DllImport("mdName", EntryPoint = "mdNameSetPrefix2", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdNameSetPrefix2(IntPtr i, IntPtr p1);
			[DllImport("mdName", EntryPoint = "mdNameSetFirstName", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdNameSetFirstName(IntPtr i, IntPtr p1);
			[DllImport("mdName", EntryPoint = "mdNameSetFirstName2", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdNameSetFirstName2(IntPtr i, IntPtr p1);
			[DllImport("mdName", EntryPoint = "mdNameSetMiddleName", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdNameSetMiddleName(IntPtr i, IntPtr p1);
			[DllImport("mdName", EntryPoint = "mdNameSetMiddleName2", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdNameSetMiddleName2(IntPtr i, IntPtr p1);
			[DllImport("mdName", EntryPoint = "mdNameSetSuffix", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdNameSetSuffix(IntPtr i, IntPtr p1);
			[DllImport("mdName", EntryPoint = "mdNameSetSuffix2", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdNameSetSuffix2(IntPtr i, IntPtr p1);
			[DllImport("mdName", EntryPoint = "mdNameSetLastName", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdNameSetLastName(IntPtr i, IntPtr p1);
			[DllImport("mdName", EntryPoint = "mdNameSetLastName2", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdNameSetLastName2(IntPtr i, IntPtr p1);
			[DllImport("mdName", EntryPoint = "mdNameParse", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdNameParse(IntPtr i);
			[DllImport("mdName", EntryPoint = "mdNameGenderize", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdNameGenderize(IntPtr i);
			[DllImport("mdName", EntryPoint = "mdNameSalutate", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdNameSalutate(IntPtr i);
			[DllImport("mdName", EntryPoint = "mdNameGetStatusCode", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdNameGetStatusCode(IntPtr i);
			[DllImport("mdName", EntryPoint = "mdNameGetErrorCode", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdNameGetErrorCode(IntPtr i);
			[DllImport("mdName", EntryPoint = "mdNameGetChangeCode", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdNameGetChangeCode(IntPtr i);
			[DllImport("mdName", EntryPoint = "mdNameGetResults", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdNameGetResults(IntPtr i);
			[DllImport("mdName", EntryPoint = "mdNameGetResultCodeDescription", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdNameGetResultCodeDescription(IntPtr i, IntPtr p1, Int32 opt);
			[DllImport("mdName", EntryPoint = "mdNameGetPrefix", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdNameGetPrefix(IntPtr i);
			[DllImport("mdName", EntryPoint = "mdNameGetPrefix2", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdNameGetPrefix2(IntPtr i);
			[DllImport("mdName", EntryPoint = "mdNameGetFirstName", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdNameGetFirstName(IntPtr i);
			[DllImport("mdName", EntryPoint = "mdNameGetFirstName2", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdNameGetFirstName2(IntPtr i);
			[DllImport("mdName", EntryPoint = "mdNameGetMiddleName", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdNameGetMiddleName(IntPtr i);
			[DllImport("mdName", EntryPoint = "mdNameGetMiddleName2", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdNameGetMiddleName2(IntPtr i);
			[DllImport("mdName", EntryPoint = "mdNameGetLastName", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdNameGetLastName(IntPtr i);
			[DllImport("mdName", EntryPoint = "mdNameGetLastName2", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdNameGetLastName2(IntPtr i);
			[DllImport("mdName", EntryPoint = "mdNameGetSuffix", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdNameGetSuffix(IntPtr i);
			[DllImport("mdName", EntryPoint = "mdNameGetSuffix2", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdNameGetSuffix2(IntPtr i);
			[DllImport("mdName", EntryPoint = "mdNameGetGender", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdNameGetGender(IntPtr i);
			[DllImport("mdName", EntryPoint = "mdNameGetGender2", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdNameGetGender2(IntPtr i);
			[DllImport("mdName", EntryPoint = "mdNameGetSalutation", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdNameGetSalutation(IntPtr i);
			[DllImport("mdName", EntryPoint = "mdNameStandardizeCompany", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdNameStandardizeCompany(IntPtr i, IntPtr p1);
			[DllImport("mdName", EntryPoint = "mdNameSetReserved", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdNameSetReserved(IntPtr i, IntPtr p1, IntPtr p2);
			[DllImport("mdName", EntryPoint = "mdNameGetReserved", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdNameGetReserved(IntPtr i, IntPtr p1);
		}

		public mdName() {
			i = mdNameUnmanaged.mdNameCreate();
		}

		~mdName() {
			Dispose();
		}

		public virtual void Dispose() {
			lock (this) {
				mdNameUnmanaged.mdNameDestroy(i);
				GC.SuppressFinalize(this);
			}
		}

		public void SetPathToNameFiles(string p1) {
			EncodedString u_p1 = new EncodedString(p1);
			mdNameUnmanaged.mdNameSetPathToNameFiles(i, u_p1.GetPtr());
		}

		public ProgramStatus InitializeDataFiles() {
			return (ProgramStatus)mdNameUnmanaged.mdNameInitializeDataFiles(i);
		}

		public string GetInitializeErrorString() {
			return EncodedString.GetEncodedString(mdNameUnmanaged.mdNameGetInitializeErrorString(i));
		}

		public int SetLicenseString(string p1) {
			EncodedString u_p1 = new EncodedString(p1);
			return mdNameUnmanaged.mdNameSetLicenseString(i, u_p1.GetPtr());
		}

		public string GetBuildNumber() {
			return EncodedString.GetEncodedString(mdNameUnmanaged.mdNameGetBuildNumber(i));
		}

		public string GetDatabaseDate() {
			return EncodedString.GetEncodedString(mdNameUnmanaged.mdNameGetDatabaseDate(i));
		}

		public string GetDatabaseExpirationDate() {
			return EncodedString.GetEncodedString(mdNameUnmanaged.mdNameGetDatabaseExpirationDate(i));
		}

		public string GetLicenseExpirationDate() {
			return EncodedString.GetEncodedString(mdNameUnmanaged.mdNameGetLicenseExpirationDate(i));
		}

		public int SetPrimaryNameHint(NameHints p1) {
			return mdNameUnmanaged.mdNameSetPrimaryNameHint(i, (int)p1);
		}

		public int SetSecondaryNameHint(NameHints p1) {
			return mdNameUnmanaged.mdNameSetSecondaryNameHint(i, (int)p1);
		}

		public int SetFirstNameSpellingCorrection(int p1) {
			return mdNameUnmanaged.mdNameSetFirstNameSpellingCorrection(i, p1);
		}

		public int SetMiddleNameLogic(MiddleNameLogic p1) {
			return mdNameUnmanaged.mdNameSetMiddleNameLogic(i, (int)p1);
		}

		public int SetGenderPopulation(Population p1) {
			return mdNameUnmanaged.mdNameSetGenderPopulation(i, (int)p1);
		}

		public int SetGenderAggression(Aggression p1) {
			return mdNameUnmanaged.mdNameSetGenderAggression(i, (int)p1);
		}

		public int AddSalutation(Salutations p1) {
			return mdNameUnmanaged.mdNameAddSalutation(i, (int)p1);
		}

		public void SetSalutationPrefix(string p1) {
			EncodedString u_p1 = new EncodedString(p1);
			mdNameUnmanaged.mdNameSetSalutationPrefix(i, u_p1.GetPtr());
		}

		public void SetSalutationSuffix(string p1) {
			EncodedString u_p1 = new EncodedString(p1);
			mdNameUnmanaged.mdNameSetSalutationSuffix(i, u_p1.GetPtr());
		}

		public void SetSalutationSlug(string p1) {
			EncodedString u_p1 = new EncodedString(p1);
			mdNameUnmanaged.mdNameSetSalutationSlug(i, u_p1.GetPtr());
		}

		public void ClearProperties() {
			mdNameUnmanaged.mdNameClearProperties(i);
		}

		public void SetFullName(string p1) {
			EncodedString u_p1 = new EncodedString(p1);
			mdNameUnmanaged.mdNameSetFullName(i, u_p1.GetPtr());
		}

		public void SetPrefix(string p1) {
			EncodedString u_p1 = new EncodedString(p1);
			mdNameUnmanaged.mdNameSetPrefix(i, u_p1.GetPtr());
		}

		public void SetPrefix2(string p1) {
			EncodedString u_p1 = new EncodedString(p1);
			mdNameUnmanaged.mdNameSetPrefix2(i, u_p1.GetPtr());
		}

		public void SetFirstName(string p1) {
			EncodedString u_p1 = new EncodedString(p1);
			mdNameUnmanaged.mdNameSetFirstName(i, u_p1.GetPtr());
		}

		public void SetFirstName2(string p1) {
			EncodedString u_p1 = new EncodedString(p1);
			mdNameUnmanaged.mdNameSetFirstName2(i, u_p1.GetPtr());
		}

		public void SetMiddleName(string p1) {
			EncodedString u_p1 = new EncodedString(p1);
			mdNameUnmanaged.mdNameSetMiddleName(i, u_p1.GetPtr());
		}

		public void SetMiddleName2(string p1) {
			EncodedString u_p1 = new EncodedString(p1);
			mdNameUnmanaged.mdNameSetMiddleName2(i, u_p1.GetPtr());
		}

		public void SetSuffix(string p1) {
			EncodedString u_p1 = new EncodedString(p1);
			mdNameUnmanaged.mdNameSetSuffix(i, u_p1.GetPtr());
		}

		public void SetSuffix2(string p1) {
			EncodedString u_p1 = new EncodedString(p1);
			mdNameUnmanaged.mdNameSetSuffix2(i, u_p1.GetPtr());
		}

		public void SetLastName(string p1) {
			EncodedString u_p1 = new EncodedString(p1);
			mdNameUnmanaged.mdNameSetLastName(i, u_p1.GetPtr());
		}

		public void SetLastName2(string p1) {
			EncodedString u_p1 = new EncodedString(p1);
			mdNameUnmanaged.mdNameSetLastName2(i, u_p1.GetPtr());
		}

		public int Parse() {
			return mdNameUnmanaged.mdNameParse(i);
		}

		public int Genderize() {
			return mdNameUnmanaged.mdNameGenderize(i);
		}

		public int Salutate() {
			return mdNameUnmanaged.mdNameSalutate(i);
		}

		public string GetStatusCode() {
			return EncodedString.GetEncodedString(mdNameUnmanaged.mdNameGetStatusCode(i));
		}

		public string GetErrorCode() {
			return EncodedString.GetEncodedString(mdNameUnmanaged.mdNameGetErrorCode(i));
		}

		public string GetChangeCode() {
			return EncodedString.GetEncodedString(mdNameUnmanaged.mdNameGetChangeCode(i));
		}

		public string GetResults() {
			return EncodedString.GetEncodedString(mdNameUnmanaged.mdNameGetResults(i));
		}

		public string GetResultCodeDescription(string p1, ResultCdDescOpt opt) {
			EncodedString u_p1 = new EncodedString(p1);
			return EncodedString.GetEncodedString(mdNameUnmanaged.mdNameGetResultCodeDescription(i, u_p1.GetPtr(), (int)opt));
		}

		public string GetPrefix() {
			return EncodedString.GetEncodedString(mdNameUnmanaged.mdNameGetPrefix(i));
		}

		public string GetPrefix2() {
			return EncodedString.GetEncodedString(mdNameUnmanaged.mdNameGetPrefix2(i));
		}

		public string GetFirstName() {
			return EncodedString.GetEncodedString(mdNameUnmanaged.mdNameGetFirstName(i));
		}

		public string GetFirstName2() {
			return EncodedString.GetEncodedString(mdNameUnmanaged.mdNameGetFirstName2(i));
		}

		public string GetMiddleName() {
			return EncodedString.GetEncodedString(mdNameUnmanaged.mdNameGetMiddleName(i));
		}

		public string GetMiddleName2() {
			return EncodedString.GetEncodedString(mdNameUnmanaged.mdNameGetMiddleName2(i));
		}

		public string GetLastName() {
			return EncodedString.GetEncodedString(mdNameUnmanaged.mdNameGetLastName(i));
		}

		public string GetLastName2() {
			return EncodedString.GetEncodedString(mdNameUnmanaged.mdNameGetLastName2(i));
		}

		public string GetSuffix() {
			return EncodedString.GetEncodedString(mdNameUnmanaged.mdNameGetSuffix(i));
		}

		public string GetSuffix2() {
			return EncodedString.GetEncodedString(mdNameUnmanaged.mdNameGetSuffix2(i));
		}

		public string GetGender() {
			return EncodedString.GetEncodedString(mdNameUnmanaged.mdNameGetGender(i));
		}

		public string GetGender2() {
			return EncodedString.GetEncodedString(mdNameUnmanaged.mdNameGetGender2(i));
		}

		public string GetSalutation() {
			return EncodedString.GetEncodedString(mdNameUnmanaged.mdNameGetSalutation(i));
		}

		public string StandardizeCompany(string p1) {
			EncodedString u_p1 = new EncodedString(p1);
			return EncodedString.GetEncodedString(mdNameUnmanaged.mdNameStandardizeCompany(i, u_p1.GetPtr()));
		}

		public void SetReserved(string p1, string p2) {
			EncodedString u_p1 = new EncodedString(p1);
			EncodedString u_p2 = new EncodedString(p2);
			mdNameUnmanaged.mdNameSetReserved(i, u_p1.GetPtr(), u_p2.GetPtr());
		}

		public string GetReserved(string p1) {
			EncodedString u_p1 = new EncodedString(p1);
			return EncodedString.GetEncodedString(mdNameUnmanaged.mdNameGetReserved(i, u_p1.GetPtr()));
		}

		private class EncodedString : IDisposable {
			private IntPtr encodedString = IntPtr.Zero;
			private static Encoding encoding = Encoding.UTF8;

			public EncodedString(string str) {
				if (str == null)
					str = "";
				byte[] buffer = encoding.GetBytes(str);
				Array.Resize(ref buffer, buffer.Length + 1);
				buffer[buffer.Length - 1] = 0;
				encodedString = Marshal.AllocHGlobal(buffer.Length);
				Marshal.Copy(buffer, 0, encodedString, buffer.Length);
			}

			~EncodedString() {
				Dispose();
			}

			public virtual void Dispose() {
				lock (this) {
					Marshal.FreeHGlobal(encodedString);
					GC.SuppressFinalize(this);
				}
			}

			public IntPtr GetPtr() {
				return encodedString;
			}

			public static string GetEncodedString(IntPtr ptr) {
				if (ptr == IntPtr.Zero)
					return "";
				int len = 0;
				while (Marshal.ReadByte(ptr, len) != 0)
					len++;
				if (len == 0)
					return "";
				byte[] buffer = new byte[len];
				Marshal.Copy(ptr, buffer, 0, len);
				return encoding.GetString(buffer);
			}
		}
	}
}
