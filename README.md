
# AutoUpdater

�T�[�o�[�ɔz�u����Ă���u�A�v��.exe�v�t�@�C���̃o�[�W�����ƁA���[�J�����s���́u�A�v��.exe�v�t�@�C���̃o�[�W�������r���āA�T�[�o�[�ɐV�����u�A�v��.exe�v������΁A�����t�H���_�[���ɑ��݂���֘A�Ώۃt�@�C���i".exe", ".config", ".manifest", ".dll", ".pdf", ".cer"�j���܂߂āA�t�H���_�[���ƃ��[�J���Ƀ_�E�����[�h���A���[�J���̃A�v���P�[�V�������ŐV�ɕۂB

�v���O�����u�A�v��.exe�v�̎��s�I�����ɁA�Ăяo���ړI�ō쐬���Ă���B�ȉ��͂��̌Ăяo���R�[�h��B

```C#
// �����A�b�v�f�[�^�����s����
try
{
    var psInfo = new ProcessStartInfo();
    psInfo.FileName = "AutoUpdater.exe"; // ���s����t�@�C��
    psInfo.CreateNoWindow = true;        // �R���\�[���E�E�B���h�E���J���Ȃ�
    psInfo.UseShellExecute = false;      // �V�F���@�\���g�p���Ȃ�
    Process.Start(psInfo);
}
catch (Exception)
{
}
```