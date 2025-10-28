using System;
using System.Runtime.InteropServices;

namespace Cobilas.GodotEngine.Utility.IO;
//
// Resumo:
//     Fornece os atributos para arquivos e diretórios.
[Serializable]
[Flags]
[ComVisible(true)]
public enum ArchiveAttributes
{
	Nil = 0,
	//
	// Resumo:
	//     O arquivo é somente leitura.
	ReadOnly = 1,
	//
	// Resumo:
	//     O arquivo está oculto e, portanto, não está incluído em uma listagem de diretório
	//     comum.
	Hidden = 2,
	//
	// Resumo:
	//     O arquivo é um arquivo do sistema. Ou seja, o arquivo faz parte do sistema operacional
	//     ou é usado exclusivamente pelo sistema operacional.
	System = 4,
	//
	// Resumo:
	//     O arquivo é um diretório.
	Directory = 0x10,
	//
	// Resumo:
	//     O arquivo é um candidato para backup ou remoção.
	Archive = 0x20,
	//
	// Resumo:
	//     Reservado para uso futuro.
	Device = 0x40,
	//
	// Resumo:
	//     O arquivo é um arquivo padrão que não tem nenhum atributo especial. Esse atributo
	//     será válido somente se for usado sozinho.
	Normal = 0x80,
	//
	// Resumo:
	//     O arquivo é temporário. Um arquivo temporário contém dados necessários durante
	//     a execução de um aplicativo, mas que não são necessários após a conclusão do
	//     aplicativo. Os sistemas de arquivos tentam manter todos os dados na memória para
	//     acesso mais rápido em vez de liberar os dados de volta para o armazenamento em
	//     massa. Um arquivo temporário deve ser excluído pelo aplicativo assim que ele
	//     não seja mais necessário.
	Temporary = 0x100,
	//
	// Resumo:
	//     O arquivo é um arquivo esparso. Em geral, arquivos esparsos são arquivos grandes
	//     cujos dados consistem principalmente em zeros.
	SparseFile = 0x200,
	//
	// Resumo:
	//     O arquivo contém um ponto de nova análise, que é um bloco de dados definidos
	//     pelo usuário associado a um arquivo ou diretório.
	ReparsePoint = 0x400,
	//
	// Resumo:
	//     O arquivo está compactado.
	Compressed = 0x800,
	//
	// Resumo:
	//     O arquivo está offline. Os dados do arquivo não estão disponíveis imediatamente.
	Offline = 0x1000,
	//
	// Resumo:
	//     O arquivo não será indexado pelo serviço de indexação de conteúdo do sistema
	//     operacional.
	NotContentIndexed = 0x2000,
	Encrypted = 0x4000,
	[ComVisible(false)]
	IntegrityStream = 0x8000,
	//
	// Resumo:
	//     O arquivo ou diretório é excluído do exame de integridade de dados. Quando esse
	//     valor é aplicado a uma pasta, por padrão, todos os novos arquivos e subdiretórios
	//     do diretório são excluídos da integridade de dados.
	[ComVisible(false)]
	NoScrubData = 0x20000
}